using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain.Recipe;

public class Tag : Entity
{
    public Tag (
        string name,
        Guid recipeId
    )
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Тег не может быть пустым");
        
        Id = Guid.NewGuid();
        Name = name;
    }
    
    public string Name { get; private set; }
    public Guid RecipeId { get; private set; }
}