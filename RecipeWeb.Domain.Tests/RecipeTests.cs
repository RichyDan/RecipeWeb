using System.Reflection;
using FluentAssertions;
using RecipeWeb.Domain.Common;
using RecipeWeb.Domain.RecipeAggregate;

namespace RecipeWeb.Domain.Tests
{
    public class RecipeTests
    {
        private readonly List<string> products = [];

        // Вспомогательный метод для создания валидных данных
        private (List<Ingredient>, List<Step>, List<Tag>) CreateDefaultCollections(
            Guid? ingId = null,
            Guid? stepId = null,
            Guid? tagId = null)
        {
            var ingredient = new Ingredient("Тестовый ингредиент", ["Продукт1", "Продукт2"]);
            SetId(ingredient, ingId ?? Guid.NewGuid());

            var step = new Step("Тестовые инструкции");
            SetId(step, stepId ?? Guid.NewGuid());

            var tag = new Tag("Тестовый Тэг");
            SetId(tag, tagId ?? Guid.NewGuid());

            return (new List<Ingredient> { ingredient }, new List<Step> { step }, new List<Tag> { tag });
        }

        // Вспомогательный метод для установки Id
        private T SetId<T>(T entity, Guid id) where T : Entity
        {
            // Использование PropertyInfo с null-conditional operator (!) для избежания NullReferenceException
            typeof(Entity).GetProperty("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)!
                .SetValue(entity, id);
            return entity;
        }

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
            Recipe recipe = new Recipe(
                name,
                description,
                timetoCook,
                countPersons,
                imagePath,
                ingredients,
                steps,
                tags);

            // Assert
            recipe.Id.Should().Be(Guid.Empty);
            recipe.Name.Should().Be(name);
            recipe.Description.Should().Be(description);
            recipe.TimeToCook.Should().Be(timetoCook);
            recipe.CountPersons.Should().Be(countPersons);
            recipe.Steps.Should().HaveCount(1);
            recipe.Tags.Should().HaveCount(1);
            recipe.Errors.Should().BeEmpty();
        }

        [Theory]
        [InlineData("", "Описание", 30, 2, "https://ok.com/img.jpg", "Name")] // Пустое название
        [InlineData("Название", "", 30, 2, "https://ok.com/img.jpg", "Description")] // Пустое описание
        [InlineData("Название", "Описание", 0, 2, "https://ok.com/img.jpg", "TimeToCook")] // Время приготовления <= 0
        [InlineData("Название", "Описание", 30, 0, "https://ok.com/img.jpg", "CountPersons")] // Кол-во персон <= 0
        [InlineData("Название", "Описание", 30, 2, "not-a-valid-url", "ImageUrl")] // Некорректный URL
        public void Constructor_Should_ThrowException_When_ValidationFails(
        string name,
        string description,
        int timeToCook,
        int countPersons,
        string imageUrl,
        string expectedErrorMessagePart)
        {
            // Arrange
            var (ingredients, steps, tags) = CreateDefaultCollections();

            // Act
            Action act = () => new Recipe(
                name,
                description,
                timeToCook,
                countPersons,
                imageUrl,
                ingredients,
                steps,
                tags);

            // Assert
            act.Should().Throw<InvalidOperationException>()
               .WithMessage($"*{expectedErrorMessagePart}*");
        }

        [Fact]
        public void Update_Should_SynchronizeCollections_When_NewItemsHaveNewIds()
        {
            // Arrange
            var (ingredients, steps, tags) = CreateDefaultCollections();
            var recipe = new Recipe("Old Name", "Old Desc", 10, 1, "https://old.com", ingredients, steps, tags);

            var newIng1 = SetId(new Ingredient("Новый ингредиент1", ["Продукт1"]), Guid.NewGuid());
            var newIng2 = SetId(new Ingredient("Новый ингредиент2", ["Продукт2"]), Guid.NewGuid());
            var newIngs = new List<Ingredient> { newIng1, newIng2 };

            var newStep = SetId(new Step("Новые инструкции"), Guid.NewGuid());
            var newSteps = new List<Step> { newStep };

            var newTag = SetId(new Tag("Новый тег"), Guid.NewGuid());
            var newTags = new List<Tag> { newTag };

            // Act
            recipe.Update("New Name", "New Desc", 20, 2, "https://new.com", newIngs, newSteps, newTags);

            // Assert
            recipe.Name.Should().Be("New Name");
            recipe.Ingredients.Should().HaveCount(2); // Старый ингредиент удален, 2 новых добавлены
            recipe.Ingredients.Should().Contain(i => i.Name == "Новый ингредиент1");
            recipe.Ingredients.First(i => i.Name == "Новый ингредиент1").Products.Should().HaveCount(1);
            recipe.Steps.Should().HaveCount(1).And.Contain(s => s.Instructions == "Новые инструкции");
            recipe.ImagePath.Should().Be("https://new.com");
        }

        [Fact]
        public void Update_Should_KeepCollection_When_NullPassed()
        {
            // Arrange
            var (ingredients, steps, tags) = CreateDefaultCollections();
            var recipe = new Recipe("Name", "Desc", 10, 1, "https://img.com", ingredients, steps, tags);

            // Act
            // Передаем null в коллекции
            recipe.Update("newName", "Desc", 10, 1, "https://img.com", ingredients: null, steps: null, tags: null);

            // Assert
            recipe.Ingredients.Should().NotBeEmpty().And.HaveCount(1);
            recipe.Ingredients.First().Products.Should().NotBeEmpty();
            recipe.Steps.Should().NotBeEmpty().And.HaveCount(1);
            recipe.Tags.Should().NotBeEmpty().And.HaveCount(1);
            recipe.Name.Should().Be("newName");
        }

        [Fact]
        public void Update_Should_ClearCollections_When_EmptyListsArePassed()
        {
            // Arrange
            var (ingredients, steps, tags) = CreateDefaultCollections();
            var recipe = new Recipe("Name", "Desc", 10, 1, "https://img.com", ingredients, steps, tags);

            // Act
            recipe.Update(
                "Name",
                "Desc",
                10,
                1,
                "https://img.com",
                ingredients: new List<Ingredient>(),
                steps: new List<Step>(),
                tags: new List<Tag>()
            );

            // Assert
            recipe.Ingredients.Should().BeEmpty();
            recipe.Steps.Should().BeEmpty();
            recipe.Tags.Should().BeEmpty();
        }

        [Fact]
        public void Update_Should_UpdateExistingIngredient_When_IdMatches()
        {
            // Arrange
            var existingIngredientId = Guid.NewGuid();
            var ingredients = new List<Ingredient>
            {
                SetId(new Ingredient("Старый ингредиент", ["Старый продукт"]), existingIngredientId)
            };

            var recipe = new Recipe("Name", "Desc", 10, 1, "https://ok.com", ingredients, [], []);

            var existingIngredient = recipe.Ingredients.First();

            // Создаем объект для обновления: тот же ID, новое имя и НОВЫЕ продукты
            var updatedIng = SetId(new Ingredient("Новое имя ингредиента", ["Новый продукт 1", "Новый продукт 2"]), existingIngredientId);
            var newIngredients = new List<Ingredient> { updatedIng };

            // Act
            recipe.Update("Name", "Desc", 10, 1, "https://ok.com", ingredients: newIngredients);

            // Assert
            recipe.Ingredients.Should().HaveCount(1);
            var resultIng = recipe.Ingredients.First();

            resultIng.Name.Should().Be("Новое имя ингредиента");
            resultIng.Products.Should().HaveCount(2).And.Contain("Новый продукт 1").And.Contain("Новый продукт 2");
            resultIng.Products.Should().NotContain("Старый продукт");

            resultIng.Should().BeSameAs(existingIngredient);
        }

        [Fact]
        public void Update_Should_HandleStepInstructionsCorrectly()
        {
            // Arrange
            var existingStepId = Guid.NewGuid();
            var steps = new List<Step> { SetId(new Step("Старый шаг"), existingStepId) };
            var recipe = new Recipe("Name", "Desc", 10, 1, "https://ok.com", [], steps, []);

            var existingStep = recipe.Steps.First();

            var updatedStep = SetId(new Step("Новое описание шага"), existingStepId);
            var newSteps = new List<Step> { updatedStep };

            // Act
            recipe.Update("Name", "Desc", 10, 1, "https://ok.com", steps: newSteps);

            // Assert
            recipe.Steps.First().Instructions.Should().Be("Новое описание шага");
            recipe.Steps.First().Should().BeSameAs(existingStep);
        }

        [Fact]
        public void Update_Should_UpdateTagNameCorrectly()
        {
            // Arrange
            var existingTagId = Guid.NewGuid();
            var tags = new List<Tag> { SetId(new Tag("Старый тег"), existingTagId) };
            var recipe = new Recipe("Name", "Desc", 10, 1, "https://ok.com", [], [], tags);

            var existingTag = recipe.Tags.First();

            var updatedTag = SetId(new Tag("Новый тег"), existingTagId);
            var newTags = new List<Tag> { updatedTag };

            // Act
            recipe.Update("Name", "Desc", 10, 1, "https://ok.com", tags: newTags);

            // Assert
            recipe.Tags.First().Name.Should().Be("Новый тег");
            recipe.Tags.First().Should().BeSameAs(existingTag);
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