using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain.Recipe;

public class Ingredient : Entity
{
    public Ingredient (
        string name, 
        List<string> products,
        Guid recipeId
        )
    {
        Id = Guid.NewGuid();
        Name = name;
        Products = products;
    }
    
    public string Name { get; private set; }
    public List<string> Products { get; private set; }
    public Guid RecipeId { get; private set; }
}