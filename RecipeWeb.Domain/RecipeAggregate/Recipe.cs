using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain.RecipeAggregate
{
    public class Recipe : Entity
    {
        private readonly List<Ingredient> _ingredients = [];
        private readonly List<Step> _steps = [];
        private readonly List<Tag> _tags = [];

        public Recipe(
            string name,
            string description,
            int timeToCook,
            int countPersons,
            string imagePath,
            IEnumerable<Ingredient> ingredients,
            IEnumerable<Step> steps,
            IEnumerable<Tag>? tags = null)
        {
            Validate(
                name,
                description,
                timeToCook,
                countPersons,
                imagePath);

            Name = name;
            Description = description;
            TimeToCook = timeToCook;
            CountPersons = countPersons;
            ImagePath = imagePath;

            if (ingredients != null)
                _ingredients.AddRange(ingredients);

            if (steps != null)
                _steps.AddRange(steps);

            _tags.AddRange(tags ?? []);
        }

        public string Name { get; private set; }
        public int TimeToCook { get; private set; }
        public int CountPersons { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }

        public IReadOnlyCollection<Ingredient> Ingredients => _ingredients.AsReadOnly();
        public IReadOnlyCollection<Step> Steps => _steps.AsReadOnly();
        public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

        public void Update(
            string name,
            string description,
            int timeToCook,
            int countPersons,
            string imagePath,
            List<Ingredient>? ingredients = null,
            List<Step>? steps = null,
            List<Tag>? tags = null)
        {
            Validate(
                name,
                description,
                timeToCook,
                countPersons,
                imagePath);

            Name = name;
            Description = description;
            TimeToCook = timeToCook;
            CountPersons = countPersons;
            ImagePath = imagePath;

            SynchronizeByContent(_ingredients, ingredients);
            SynchronizeByContent(_steps, steps);
            SynchronizeByContent(_tags, tags);
        }

        private void SynchronizeByContent<T>(
            List<T> internalList,
            IEnumerable<T> newList)
            where T : class
        {
            if (newList == null)
                return;

            // Удаляем элементы, которых нет в новом списке (сравнение через Equals)
            internalList.RemoveAll(item => !newList.Contains(item));

            // Добавляем те, которых еще нет во внутреннем списке
            foreach (T newItem in newList)
            {
                if (!internalList.Contains(newItem))
                    internalList.Add(newItem);
            }
        }

        private void Validate(
            string name,
            string description,
            int timeToCook,
            int countPersons,
            string? imageUrl)
        {
            if (string.IsNullOrWhiteSpace(name))
                AddError(nameof(name), "Название рецепта не может быть пустым");

            if (string.IsNullOrWhiteSpace(description))
                AddError(nameof(description), "Описание не может быть пустым");

            if (timeToCook == 0)
                AddError(nameof(timeToCook), "Время приготовления блюда не может быть равным 0");

            if (countPersons == 0)
                AddError(nameof(countPersons), "Количество персон должно быть больше 0");

            if (!string.IsNullOrEmpty(imageUrl) && !Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
                AddError(nameof(imageUrl), "Некорректный формат URL картинки");

            // Вызов единого метода вывода ошибок
            EnsureValid();
        }
    }
}