using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain;

public class Tag : Entity
{
    public Tag (
        string name,
        Guid recipeId
    )
    {
        Id = Guid.NewGuid();
        Name = name;
    }
    
    public string Name { get; private set; }
    public Guid RecipeId { get; private set; }
}