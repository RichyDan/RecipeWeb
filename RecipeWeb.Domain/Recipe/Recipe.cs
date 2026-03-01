using RecipeWeb.Domain.Common;
using RecipeWeb.Domain.User;

namespace RecipeWeb.Domain.Recipe;

public class Recipe : Entity
{
    public Recipe (
        string name, 
        string description,
        Int32 timeToCook, 
        Int32 countPersons,
        string imagePath,
        List<Ingredient> ingredients,
        List<Step> steps,
        List<Tag>? tags = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        TimeToCook = timeToCook;
        CountPersons = countPersons;
        ImagePath = ImagePath;

        Ingredients = ingredients;
        Steps = steps;
        Tags = tags ?? new List<Tag>();
    }
    
    public string Name { get; private set; }
    public Int32 TimeToCook { get; private set; }
    public Int32 CountPersons { get; private set; }
    public string Description { get; private set; }
    public string ImagePath { get; private set; }
    public List<Ingredient> Ingredients { get; private set; }
    public List<Step> Steps { get; private set; }
    public List<Tag> Tags { get; private set; }
    
    public void Update(
        string name, 
        string description, 
        Int32 timeToCook, 
        Int32 countPersons,
        string imagePath,
        List<Ingredient>? ingredients = null,
        List<Step>? steps = null,
        List<Tag>? tags = null)
    {
        Name = name;
        Description = description;
        TimeToCook = timeToCook;
        CountPersons = countPersons;
        ImagePath = imagePath;
        
        if (ingredients != null)
        {
            Ingredients.Clear();
            Ingredients.AddRange(ingredients);
        }
        
        if (steps != null)
        {
            Steps.Clear();
            Steps.AddRange(steps);
        }
        
        if (tags != null)
        {
            Tags.Clear();
            Tags.AddRange(tags);
        }
    }
}