using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain.Recipeaggrigate;

public class Step : Entity
{
    public Step (
        string instructions
    )
    {
        ClearErrors();
        if (string.IsNullOrWhiteSpace(instructions))
            AddError(nameof(instructions), "Инструкции не могут быть пустыми");
        EnsureValid();
        
        Instructions = instructions;
    }
    
    public string Instructions { get; private set; }
}