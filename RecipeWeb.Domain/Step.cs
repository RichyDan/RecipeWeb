using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain;

public class Step : Entity
{
    public Step (
        List<string> instructions,
        Guid recipeId
    )
    {
        Id = Guid.NewGuid();
        Instructions = instructions;
    }
    
    public List<string> Instructions { get; private set; }
    public Guid RecipeId { get; private set; }
}