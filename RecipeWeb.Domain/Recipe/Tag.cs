using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain.Recipe;

public class Tag : Entity
{
    public Tag (
        string name
    ) : base()
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Тег не может быть пустым");
        
        Name = name;
    }
    
    public string Name { get; private set; }
}