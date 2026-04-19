using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain.RecipeAggregate
{
    public class Step : Entity
    {
        public string Instructions { get; private set; }

        public Step(string instructions) => Update(instructions);

        public void Update(string instructions)
        {
            if (string.IsNullOrWhiteSpace(instructions))
                AddError(nameof(Instructions), "Инструкции не могут быть пустыми");

            EnsureValid();

            Instructions = instructions;
        }

        public bool Equals(Step? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Instructions == other.Instructions;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Step other) return false;
            return Equals(other);
        }

        public override int GetHashCode() => Instructions?.GetHashCode() ?? 0;
    }
}