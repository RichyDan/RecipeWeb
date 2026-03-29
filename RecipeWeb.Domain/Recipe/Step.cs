using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain.Recipe;

public class Step : Entity
{
    public Step (
        string instructions
    ) : base()
    {
        if (string.IsNullOrWhiteSpace(instructions))
            throw new ArgumentException("Инструкции не могут быть пустыми");
        
        Instructions = instructions;
    }
    
    public string Instructions { get; private set; }
}