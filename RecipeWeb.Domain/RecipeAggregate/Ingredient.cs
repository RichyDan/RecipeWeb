using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain.RecipeAggregate
{
    public class Ingredient : Entity
    {
        private readonly List<string> _products = [];
        public IReadOnlyCollection<string> Products => _products.AsReadOnly();
        public string Name { get; private set; } = null!;

        public Ingredient(string name, IEnumerable<string> products) => Update(name, products);

        public void Update(string name, IEnumerable<string> products)
        {
            ClearErrors();

            if (string.IsNullOrWhiteSpace(name))
                AddError(nameof(Name), "Название ингредиента не может быть пустым");

            if (products == null)
                AddError(nameof(_products), "Список продуктов не может быть пустым");

            EnsureValid();

            Name = name;
            _products.Clear();
            _products.AddRange(products);
        }

        public void AddProduct(string product)
        {
            ClearErrors();
            if (string.IsNullOrWhiteSpace(product))
                AddError(nameof(product), "Название продукта не может быть пустым");
            EnsureValid();

            if (!_products.Contains(product))
            {
                _products.Add(product);
            }
        }
        public override bool Equals(object? obj)
        {
            if (obj is not Ingredient other)
                return false;
            return Name == other.Name && Products.SequenceEqual(other.Products);
        }

        public void RemoveProduct(string product) => _products.Remove(product);
    }
}