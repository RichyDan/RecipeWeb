using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain.Recipe;

public class Step : Entity
{
    public Step (
        Guid recipeId,
        string instructions
    )
    {
        Id = Guid.NewGuid();
        Instructions = instructions;
    }

    public Guid RecipeId { get; private set; }
    public string Instructions { get; private set; }
}