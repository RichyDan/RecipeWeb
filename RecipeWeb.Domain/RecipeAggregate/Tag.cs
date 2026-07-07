using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain.RecipeAggregate;

public class Tag : Entity
{
    private Tag() { }

    public Tag(string name) => Update(name);

    public string Name { get; private set; }

    public void Update(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            AddError(nameof(Name), "Название тега не может быть пустым");
        EnsureValid();

        Name = name;
    }

    public bool Equals(Tag? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Tag other) return false;
        return Equals(other);
    }

    public override int GetHashCode() => Name?.GetHashCode() ?? 0;
}