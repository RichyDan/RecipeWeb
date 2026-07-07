using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain.RecipeAggregate;

public class Ingredient : Entity, IEquatable<Ingredient>
{
    private readonly List<string> products =[];

    private Ingredient()
    {
    }

    public Ingredient(string name, IEnumerable<string> products) => this.Update(name, products);

    public IReadOnlyCollection<string> Products => this.products.AsReadOnly();

    public string Name { get; private set; } = null!;

    public void Update(string name, IEnumerable<string> products)
    {
        this.ClearErrors();

        if (string.IsNullOrWhiteSpace(name))
        {
            this.AddError(nameof(this.Name), "Название ингредиента не может быть пустым");
        }

        if (products == null)
        {
            this.AddError(nameof(this.products), "Список продуктов не может быть пустым");
        }

        this.EnsureValid();

        this.Name = name;
        this.products.Clear();
        this.products.AddRange(products);
    }

    public void AddProduct(string product)
    {
        this.ClearErrors();
        if (string.IsNullOrWhiteSpace(product))
        {
            this.AddError(nameof(product), "Название продукта не может быть пустым");
        }

        this.EnsureValid();

        if (!this.products.Contains(product))
        {
            this.products.Add(product);
        }
    }

    public void RemoveProduct(string product) => this.products.Remove(product);

    public bool Equals(Ingredient? otherIngredient)
    {
        if (otherIngredient is null)
        {
            return false;
        }

        if (ReferenceEquals(this, otherIngredient))
        {
            return true; // ссылка на один и тот же объект
        }

        // Сравнение по содержанию
        return this.Name == otherIngredient.Name &&
               this.products.SequenceEqual(otherIngredient.products);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Ingredient other)
        {
            return false;
        }

        return this.Equals(other);
    }

    public override int GetHashCode()
    {
        var hash = default( HashCode );
        hash.Add(this.Name);
        foreach (var product in this.products)
        {
            hash.Add(product);
        }

        return hash.ToHashCode();
    }
}