using FluentAssertions;

namespace RecipeWeb.Domain.RecipeAggregate.Tests;

public class RecipeTests
{
    // Тестовый ID автора
    private static readonly Guid TestAuthorId = Guid.NewGuid();

    // Вспомогательный метод для создания начальных данных
    private (List<Ingredient>, List<Step>, List<Tag>) CreateDefaultCollections() => (
        new List<Ingredient> { new Ingredient( "Мука", ["Пшеница"] ) },
        new List<Step> { new Step( "Замесить тесто" ) },
        new List<Tag> { new Tag( "Выпечка" ) });

    [Fact]
    public void Recipe_Should_BeCreated_Correctly()
    {
        var (ingredients, steps, tags) = this.CreateDefaultCollections();

        var recipe = new Recipe(
            "Борщ",
            "Описание",
            60,
            4,
            "https://img.com/1.jpg",
            TestAuthorId,
            ingredients,
            steps,
            tags );

        // Проверки создания
        recipe.Id.Should().Be( Guid.Empty );
        recipe.Name.Should().Be( "Борщ" );
        recipe.Ingredients.Should().HaveCount( 1 );
        recipe.AuthorId.Should().Be( TestAuthorId );
        recipe.Steps.Should().HaveCount( 1 );
        recipe.Tags.Should().HaveCount( 1 );
    }

    [Fact]
    public void Recipe_Update_Should_ModifyAllFieldsAndSynchronizeCollections()
    {
        // Arrange
        var (initialIngredients, initialSteps, initialTags) = this.CreateDefaultCollections();
        var initialIngredient = initialIngredients[0];

        var recipe = new Recipe(
            "Борщ",
            "Описание",
            10,
            1,
            "https://old.com",
            TestAuthorId,
            initialIngredients,
            initialSteps,
            initialTags );

        var newIngredients = new List<Ingredient>
        {
            new Ingredient("Мука",["Пшеница"]),
            new Ingredient("Соль",["Морская"]),
        };

        var newSteps = new List<Step> { new Step( "Новый шаг" ) };
        var newTags = new List<Tag> { new Tag( "Веган" ) };

        // Act
        recipe.Update(
            "Новое имя",
            "Новое описание",
            60,
            4,
            "https://new.com/2.jpg",
            newIngredients,
            newSteps,
            newTags );

        // Assert
        // Проверка простых полей
        recipe.Name.Should().Be( "Новое имя" );
        recipe.Description.Should().Be( "Новое описание" );
        recipe.TimeToCook.Should().Be( 60 );
        recipe.CountPersons.Should().Be( 4 );
        recipe.ImagePath.Should().Be( "https://new.com/2.jpg" );
        recipe.AuthorId.Should().Be( TestAuthorId );

        // Проверка синхронизации ингредиентов
        recipe.Ingredients.Should().HaveCount( 2 );
        recipe.Ingredients.Should().Contain( i => i.Name == "Соль" );

        // Проверка сохранения ссылки для неизмененного контента
        recipe.Ingredients.First( i => i.Name == "Мука" ).Should().BeSameAs( initialIngredient );

        // Проверка шагов и тегов
        recipe.Steps.Should().HaveCount( 1 ).And.ContainSingle( s => s.Instructions == "Новый шаг" );
        recipe.Tags.Should().HaveCount( 1 ).And.ContainSingle( t => t.Name == "Веган" );
        recipe.Tags.Should().NotContain( t => t.Name == "Выпечка" );
    }

    [Fact]
    public void Recipe_Update_Should_KeepExistingCollections_When_NullIsPassed()
    {
        // Arrange
        var (ingredients, steps, tags) = this.CreateDefaultCollections();

        var recipe = new Recipe(
            "Борщ",
            "Описание",
            10,
            1,
            "https://123.com",
            TestAuthorId,
            ingredients,
            steps,
            tags );

        // Act
        recipe.Update(
            recipe.Name,
            recipe.Description,
            recipe.TimeToCook,
            recipe.CountPersons,
            recipe.ImagePath,
            ingredients: null,
            steps: null,
            tags: null );

        // Assert
        // Коллекции не должны измениться или очиститься
        recipe.Ingredients.Should().HaveCount( 1 );
        recipe.Steps.Should().HaveCount( 1 );
        recipe.Tags.Should().HaveCount( 1 );
    }

    [Theory]
    [InlineData( "", "Описание", 30, 2, "https://ok.com", "Name" )]
    [InlineData( "Название", "", 30, 2, "https://ok.com", "Description" )]
    [InlineData( "Название", "Описание", 0, 2, "https://ok.com", "TimeToCook" )]
    [InlineData( "Название", "Описание", 30, 0, "https://ok.com", "CountPersons" )]
    [InlineData( "Название", "Описание", 30, 2, "not-a-url", "imageUrl" )]
    public void Recipe_Should_ThrowException_When_ValidationFails(
        string name,
        string desc,
        int time,
        int persons,
        string url,
        string errorPart )
    {
        // Act
        Action act = () => new Recipe(
            name,
            desc,
            time,
            persons,
            url,
            Guid.NewGuid(),
            [],
            [],
            [] );

        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage( $"*{errorPart}*" );
    }
}