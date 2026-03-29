using RecipeWeb.Domain.Common;
using RecipeWeb.Domain.Useraggragate;

namespace RecipeWeb.Domain.Recipe;

public class Recipe : Entity
{
    private readonly List<Ingredient> _ingredients = new();
    private readonly List<Step> _steps = new();
    private readonly List<Tag> _tags = new();
    
    private void Validate(string name, string description, uint timeToCook, uint countPersons, string? imageUrl)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название рецепта не может быть пустым");

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Описание не может быть пустым");

        if (timeToCook <= 0)
            throw new ArgumentException("Время приготовления должно быть больше нуля");

        if (countPersons <= 0)
            throw new ArgumentException("Количество персон должно быть не менее 1");

        if (!string.IsNullOrEmpty(imageUrl) && !Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
            throw new ArgumentException("Некорректный формат URL картинки");
    }
    
    public Recipe (
        string name, 
        string description,
        uint timeToCook, 
        uint countPersons,
        string imagePath,
        IEnumerable<Ingredient> ingredients,
        IEnumerable<Step> steps,
        IEnumerable<Tag>? tags = null)
    {
        Validate(name, description, timeToCook, countPersons, imagePath);
        
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        TimeToCook = timeToCook;
        CountPersons = countPersons;
        ImagePath = imagePath;
        
        _ingredients.AddRange(ingredients);
        _steps.AddRange(steps);
        _tags.AddRange(tags ?? new List<Tag>());
    }
    
    public string Name { get; private set; }
    public uint TimeToCook { get; private set; }
    public uint CountPersons { get; private set; }
    public string Description { get; private set; }
    public string ImagePath { get; private set; }
    
    public IReadOnlyCollection<Ingredient> Ingredients => _ingredients.AsReadOnly();
    public IReadOnlyCollection<Step> Steps => _steps.AsReadOnly();
    public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();
    
    public void Update(
        string name, 
        string description, 
        uint timeToCook, 
        uint countPersons,
        string imagePath,
        List<Ingredient>? ingredients = null,
        List<Step>? steps = null,
        List<Tag>? tags = null)
    {
        Validate(name, description, timeToCook, countPersons, imagePath);
        
        Name = name;
        Description = description;
        TimeToCook = timeToCook;
        CountPersons = countPersons;
        ImagePath = imagePath;

        if (ingredients != null)
        {
            _ingredients.Clear();
            _ingredients.AddRange(ingredients);
        }
        else
        {
            _ingredients.Clear();
        }
        
        if (steps != null)
        {
            _steps.Clear();
            _steps.AddRange(steps);
        }
        else
        {
            _steps.Clear();
        }

        if (tags != null)
        {
            _tags.Clear();
            _tags.AddRange(tags);
        }  
        else
        {
           _tags.Clear(); 
        }
    }
}