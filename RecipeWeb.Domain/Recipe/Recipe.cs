using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain.Recipe;

public class Recipe : Entity
{
    public Recipe (
        string name, 
        string description,
        string timeToCook, 
        string countPersons,
        string imageUrl)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        TimeToCook = timeToCook;
        CountPersons = countPersons;
        ImageUrl = imageUrl;
    }
    
    public string Name { get; private set; }
    public string TimeToCook { get; private set; }
    public string CountPersons { get; private set; }
    public string Description { get; private set; }
    public string ImageUrl { get; private set; }
    
    public void Update(
        string name, 
        string description, 
        string timeToCook, 
        string countPersons,
        string imageUrl)
    {
        Name = name;
        Description = description;
        TimeToCook = timeToCook;
        CountPersons = countPersons;
        ImageUrl = imageUrl;
    }
}