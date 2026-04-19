using FluentAssertions;

namespace RecipeWeb.Domain.RecipeAggregate.Tests
{
    public class RecipeTests
    {
        // Вспомогательный метод для создания начальных данных
        private (List<Ingredient>, List<Step>, List<Tag>) CreateDefaultCollections() => (
            new List<Ingredient> { new Ingredient("Мука", ["Пшеница"]) },
            new List<Step> { new Step("Замесить тесто") },
            new List<Tag> { new Tag("Выпечка") }
        );

        [Fact]
        public void Recipe_Should_BeCreated_And_Updated_Correctly()
        {
            // ТЕСТ СОЗДАНИЯ (CREATE)
            var (ingredients, steps, tags) = CreateDefaultCollections();
            var initialIngredient = ingredients[0];

            var recipe = new Recipe(
                "Старое имя",
                "Старое описание",
                30,
                2,
                "https://old.com/1.jpg",
                ingredients,
                steps,
                tags);

            // Проверки создания
            recipe.Id.Should().Be(Guid.Empty);
            recipe.Name.Should().Be("Старое имя");
            recipe.Ingredients.Should().HaveCount(1);

            // ТЕСТ ПОЛНОГО ОБНОВЛЕНИЯ (UPDATE)
            var newIngredients = new List<Ingredient>
        {
            new Ingredient("Мука", ["Пшеница"]), // Такое же содержание
            new Ingredient("Соль", ["Морская"])  // Новое
        };
            var newSteps = new List<Step> { new Step("Новый шаг") };
            var newTags = new List<Tag> { new Tag("Веган") };

            recipe.Update(
                "Новое имя",
                "Новое описание",
                60,
                4,
                "https://new.com/2.jpg",
                newIngredients,
                newSteps,
                newTags);

            // Проверки полей
            recipe.Name.Should().Be("Новое имя");
            recipe.Description.Should().Be("Новое описание");
            recipe.TimeToCook.Should().Be(60);
            recipe.CountPersons.Should().Be(4);
            recipe.ImagePath.Should().Be("https://new.com/2.jpg");

            // Проверки коллекций
            recipe.Ingredients.Should().HaveCount(2);
            recipe.Ingredients.Should().Contain(i => i.Name == "Соль");

            // КЛЮЧЕВАЯ ПРОВЕРКА: Ингредиент с тем же содержанием не был пересоздан
            recipe.Ingredients.First(i => i.Name == "Мука").Should().BeSameAs(initialIngredient);

            recipe.Steps.Should().HaveCount(1);
            recipe.Steps.First().Instructions.Should().Be("Новый шаг");

            recipe.Tags.Should().HaveCount(1);
            recipe.Tags.First().Name.Should().Be("Веган");
            recipe.Tags.Should().NotContain(t => t.Name == "Выпечка");

            // ТЕСТ СОХРАНЕНИЯ ДАННЫХ ПРИ NULL
            recipe.Update(
                recipe.Name,
                recipe.Description,
                recipe.TimeToCook,
                recipe.CountPersons,
                recipe.ImagePath,
                ingredients: null,
                steps: null,
                tags: null);

            recipe.Ingredients.Should().HaveCount(2); // Ничего не удалилось
        }

        [Theory]
        [InlineData("", "Описание", 30, 2, "https://ok.com", "Name")]
        [InlineData("Название", "", 30, 2, "https://ok.com", "Description")]
        [InlineData("Название", "Описание", 0, 2, "https://ok.com", "TimeToCook")]
        [InlineData("Название", "Описание", 30, 0, "https://ok.com", "CountPersons")]
        [InlineData("Название", "Описание", 30, 2, "not-a-url", "imageUrl")]
        public void Recipe_Should_ThrowException_When_ValidationFails(
            string name,
            string desc,
            int time,
            int persons,
            string url,
            string errorPart)
        {
            // Act
            Action act = () => new Recipe(
                name,
                desc,
                time,
                persons,
                url,
                [],
                [],
                []);

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage($"*{errorPart}*");
        }
    }
}