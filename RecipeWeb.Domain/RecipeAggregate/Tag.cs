using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain.RecipeAggregate
{
    public class Tag : Entity
    {
        public Tag(string name) => Update(name);
        public string Name { get; private set; }

        public void Update(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                AddError(nameof(Name), "Название тега не может быть пустым");
            EnsureValid();

            Name = name;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Tag other)
                return false;
            return Name == other.Name;
        }
    }
}