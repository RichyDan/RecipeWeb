using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain.RecipeAggregate;

public class Step : Entity
{
    private Step()
    {
    }

    public Step(string instructions) => this.Update(instructions);

    public string Instructions { get; private set; }

    public void Update(string instructions)
    {
        if (string.IsNullOrWhiteSpace(instructions))
        {
            this.AddError(nameof(this.Instructions), "Инструкции не могут быть пустыми");
        }

        this.EnsureValid();

        this.Instructions = instructions;
    }

    public bool Equals(Step? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return this.Instructions == other.Instructions;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Step other)
        {
            return false;
        }

        return this.Equals(other);
    }

    public override int GetHashCode() => this.Instructions?.GetHashCode() ?? 0;
}