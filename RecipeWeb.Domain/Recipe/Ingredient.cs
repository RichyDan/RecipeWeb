using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain.Recipe;

public class Ingredient : Entity
{
    private readonly List<string> _products = new();
    public Ingredient (
        string name, 
        IEnumerable<string>? products)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название ингредиента не может быть пустым");
        
        Id = Guid.NewGuid();
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
        if (product == String.Empty) throw new ArgumentNullException(nameof(product));

        if (!_products.Contains(product))
        {
            _products.Add(product);
        }
    }

    public void RemoveProduct(string product)
    {
        _products.Remove(product);
    }
}