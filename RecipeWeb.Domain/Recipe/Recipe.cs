using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain.Recipe;

public class Recipe : Entity
{111
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
        
        Ingredients = new List<Ingredient>(); // Инициализация пустых списков
        Tags = new List<Tag>();
        Steps = new List<Step>();
    }
    
    public string Name { get; private set; }
    public string TimeToCook { get; private set; }
    public string CountPersons { get; private set; }
    public string Description { get; private set; }
    public string ImageUrl { get; private set; }
    public List<Ingredient> Ingredients { get; private set; }
    public List<Step> Steps { get; private set; }
    public List<Tag> Tags { get; private set; }
    
    public void Update(
        string name, 
        string description, 
        string timeToCook, 
        string countPersons,
        string imageUrl,
        List<Ingredient> newIngredients = null,
        List<Tag> newTags = null,
        List<Step> newSteps = null)
    {
        Name = name;
        Description = description;
        TimeToCook = timeToCook;
        CountPersons = countPersons;
        ImageUrl = imageUrl;
        
        if (newIngredients != null)
        {
            Ingredients.Clear();
            Ingredients.AddRange(newIngredients);
        }

        if (newTags != null)
        {
            Tags.Clear();
            Tags.AddRange(newTags);
        }

        if (newSteps != null)
        {
            Steps.Clear();
            Steps.AddRange(newSteps);
        }
    }
}