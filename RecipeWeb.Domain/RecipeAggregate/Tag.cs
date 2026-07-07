using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain.RecipeAggregate;

public class Tag : Entity
{
    private Tag()
    {
    }

    public Tag(string name) => this.Update(name);

    public string Name { get; private set; }

    public void Update(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            this.AddError(nameof(this.Name), "Название тега не может быть пустым");
        }

        this.EnsureValid();

        this.Name = name;
    }

    public bool Equals(Tag? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return this.Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Tag other)
        {
            return false;
        }

        return this.Equals(other);
    }

    public override int GetHashCode() => this.Name?.GetHashCode() ?? 0;
}