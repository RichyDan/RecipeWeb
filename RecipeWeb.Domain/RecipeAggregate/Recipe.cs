using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain.RecipeAggregate
{
    public class Recipe : Entity
    {
        private readonly List<Ingredient> _ingredients = [];
        private readonly List<Step> _steps = [];
        private readonly List<Tag> _tags = [];

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

        private static void SynchronizeCollections<T>(
            List<T> oldlList,
            IEnumerable<T> newList,
            Action<T, T>? updateAction = null) where T : Entity
        {
            // Определяем ID объектов, которые пришли в новом списке
            var newIds = newList.Select(x => x.Id).ToHashSet();

            // Удаляем из внутреннего списка те элементы, которых нет в новом
            oldlList.RemoveAll(x => !newIds.Contains(x.Id));

            // Обрабатываем добавление и обновление
            foreach (T? newItem in newList)
            {
                T? existingItem = oldlList.FirstOrDefault(x => x.Id == newItem.Id);

                if (existingItem != null)
                    // Элемент уже есть — обновляем его поля через переданный делегат (Update)
                    updateAction?.Invoke(existingItem, newItem);
                else
                    oldlList.Add(newItem);
            }
        }

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
            Validate(name,
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
            Validate(name, description, timeToCook, countPersons, imagePath);

            Name = name;
            Description = description;
            TimeToCook = timeToCook;
            CountPersons = countPersons;
            ImagePath = imagePath;

            if (ingredients != null)
                SynchronizeCollections(
                    _ingredients,
                    ingredients,
                    (oldInggredients, newInggredients) =>
                        oldInggredients.Update(newInggredients.Name, newInggredients.Products));

            if (steps != null)
                SynchronizeCollections(
                    _steps,
                    steps,
                    (oldStep, newStep) =>
                        oldStep.Update(newStep.Instructions));

            if (tags != null)
                SynchronizeCollections(
                    _tags,
                    tags,
                    (oldTag, newTag) =>
                        oldTag.Update(newTag.Name));
        }
    }
}