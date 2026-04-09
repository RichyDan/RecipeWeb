using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain.RecipeAggregate
{
    public class Step : Entity
    {
        public string Instructions { get; private set; }

        public Step(string instructions) => Update(instructions);

        public void Update(string instructions)
        {
            ClearErrors();

            if (string.IsNullOrWhiteSpace(instructions))
                AddError(nameof(Instructions), "Инструкции не могут быть пустыми");

            EnsureValid();

            Instructions = instructions;
        }
    }
}