using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain.Recipeaggrigate
{
    public class Ingredient : Entity
    {
        private readonly List<string> _products = [];
        public Ingredient(
            string name,
            IEnumerable<string>? products)
        {
            // validation
            ClearErrors();
            if (string.IsNullOrWhiteSpace(name))
                AddError(nameof(name), "Название ингредиента не может быть пустым");
            EnsureValid();

            Name = name;

            if (products != null)
            {
                _products.AddRange(products);
            }
        }
        public IReadOnlyCollection<string> Products => _products.AsReadOnly();
        public string Name { get; private set; }

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

        public void RemoveProduct(string product) => _products.Remove(product);
    }
}