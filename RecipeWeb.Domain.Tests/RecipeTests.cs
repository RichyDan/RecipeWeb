using FluentAssertions;
using RecipeWeb.Domain.Recipeaggrigate;

namespace RecipeWeb.Domain.Tests
{
    public class RecipeTests
    {
        private readonly List<string> products = [];

        // Вспомогательный метод для создания валидных данных
        private (List<Ingredient>, List<Step>, List<Tag>) CreateDefaultCollections() => (
            new List<Ingredient> { new Ingredient("Тестовый ингредиент", new List<string> { "Продукт1", "Продукт2", "Продукт3" }) },
            new List<Step> { new Step("Тестовые инструкции") },
            new List<Tag> { new Tag("Тестовый Тэг") }
        );

        [Fact]
        public void Create_Recipe()
        {
            // Arrange
            var (ingredients, steps, tags) = CreateDefaultCollections();

            string name = "Борщ";
            string description = "Традиционный суп";
            int timetoCook = 45;
            int countPersons = 2;
            string imagePath = @"https://recipe.com";

            // Act
            Recipe recipe = new Recipe(name, description, timetoCook, countPersons, imagePath, ingredients, steps, tags);

            // Assert
            recipe.Id.Should().NotBeEmpty();
            recipe.Name.Should().Be(name);
            recipe.Description.Should().Be(description);
            recipe.TimeToCook.Should().Be(timetoCook);
            recipe.CountPersons.Should().Be(countPersons);
            recipe.Steps.Should().HaveCount(1);
            recipe.Tags.Should().HaveCount(1);
            recipe.Errors.Should().BeEmpty();
        }

        [Theory]
        [InlineData("", "Описание", "Название")] // Пустое имя
        [InlineData("Название", "", "Описание")] // Пустое описание
        [InlineData("Название", "Описание", "imageUrl")] // Некорректный URL
        public void Constructor_Should_ThrowException_When_ValidationFails(string name, string description, string expectedErrorPart)
        {
            // Arrange
            var (ingredients, steps, tags) = CreateDefaultCollections();
            var invalidUrl = "not-a-url";

            // Act
            Action act = () => new Recipe(name, description, 30, 2,
                expectedErrorPart == "imageUrl" ? invalidUrl : "https://ok.com", ingredients, steps, tags);

            // Assert
            act.Should().Throw<InvalidOperationException>()
               .WithMessage($"*{expectedErrorPart}*");
        }

        [Fact]
        public void Update_Should_ModifyProperties_And_ReplaceCollections()
        {
            // Arrange
            var (ingredients, steps, tags) = CreateDefaultCollections();
            var recipe = new Recipe("Old Name", "Old Desc", 10, 1, "https://old.com", ingredients, steps, tags);

            var newIng = new List<Ingredient>
            {
                new Ingredient("Тестовый ингредиент1", new List<string> { "Продукт1", "Продукт2", "Продукт3" }),
                new Ingredient("Тестовый ингредиент2", new List<string> { "Продукт4", "Продукт5", "Продукт6" }) };
            var newSteps = new List<Step> { new Step("Инстуркции2") };
            var newTags = new List<Tag> { new Tag("Тэг2") };

            // Act
            recipe.Update("New Name", "New Desc", 20, 2, "https://new.com", newIng, newSteps, newTags);

            // Assert
            recipe.Name.Should().Be("New Name");
            recipe.Ingredients.Should().HaveCount(2); // Старый ингредиент удален, 2 новых добавлены
            recipe.Steps.Should().HaveCount(1);
            recipe.ImagePath.Should().Be("https://new.com");
        }

        [Fact]
        public void Update_Should_ClearCollections_When_NullPassed()
        {
            // Arrange
            var (ingredients, steps, tags) = CreateDefaultCollections();
            var recipe = new Recipe("Name", "Desc", 10, 1, "https://img.com", ingredients, steps, tags);

            // Act
            // Передаем null в коллекции
            recipe.Update("Name", "Desc", 10, 1, "https://img.com", ingredients: null, steps: null, tags: null);

            // Assert
            recipe.Ingredients.Should().BeEmpty();
            recipe.Steps.Should().BeEmpty();
            recipe.Tags.Should().BeEmpty();
        }

        [Fact]
        public void Validate_Should_Handle_Null_ImageUrl_Correctly()
        {
            // Arrange
            var (inggredients, steps, tags) = CreateDefaultCollections();

            // Act
            var recipe = new Recipe("Name", "Desc", 10, 1, imagePath: null, inggredients, steps, tags);

            // Assert
            recipe.Errors.Should().BeEmpty();
            recipe.ImagePath.Should().BeNull();
        }
    }
}