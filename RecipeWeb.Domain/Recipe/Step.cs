using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain.Recipe;

public class Step : Entity
{
    public Step (
        Guid recipeId,
        string instructions
    )
    {
        if (string.IsNullOrWhiteSpace(instructions))
            throw new ArgumentException("Инструкции не могут быть пустыми");
        
        Id = Guid.NewGuid();
        Instructions = instructions;
    }

    public Guid RecipeId { get; private set; }
    public string Instructions { get; private set; }
}