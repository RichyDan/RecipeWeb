using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain.Recipeaggrigate;

public class Tag : Entity
{
    public Tag (
        string name
    )
    {
        ClearErrors();
        if (string.IsNullOrWhiteSpace(name))
            AddError(nameof(name), "Тег не может быть пустым");
        EnsureValid();
        
        Name = name;
    }
    
    public string Name { get; private set; }
}