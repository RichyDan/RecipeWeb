using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain.RecipeAggregate;

public class Ingredient : Entity, IEquatable<Ingredient>
{
    private readonly List<string> _products = [];

    private Ingredient() { }

    public Ingredient(string name, IEnumerable<string> products) => Update(name, products);

    public IReadOnlyCollection<string> Products => _products.AsReadOnly();
    public string Name { get; private set; } = null!;

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

    public void RemoveProduct(string product) => _products.Remove(product);

    public bool Equals(Ingredient? otherIngredient)
    {
        if (otherIngredient is null) return false;
        if (ReferenceEquals(this, otherIngredient)) return true; // ссылка на один и тот же объект

        // Сравнение по содержанию
        return Name == otherIngredient.Name && 
               _products.SequenceEqual(otherIngredient._products);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Ingredient other) return false;
        return Equals(other);
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(Name);
        foreach (var product in _products)
        {
            hash.Add(product);
        }
        return hash.ToHashCode();
    }
}