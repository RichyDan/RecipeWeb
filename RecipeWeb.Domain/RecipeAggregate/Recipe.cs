using RecipeWeb.Domain.Common;
using RecipeWeb.Lib;

namespace RecipeWeb.Domain.RecipeAggregate;

public class Recipe : Entity
{
    private readonly List<Ingredient> ingredients = [];
    private readonly List<Step> steps = [];
    private readonly List<Tag> tags = [];

    private Recipe()
    {
    }

    public Recipe(
        string name,
        string description,
        int timeToCook,
        int countPersons,
        string imagePath,
        Guid authorId,
        IEnumerable<Ingredient> ingredients,
        IEnumerable<Step> steps,
        IEnumerable<Tag>? tags = null )
    {
        this.Validate(
            name,
            description,
            timeToCook,
            countPersons,
            imagePath );

        this.Name = name;
        this.Description = description;
        this.TimeToCook = timeToCook;
        this.CountPersons = countPersons;
        this.ImagePath = imagePath;
        this.AuthorId = authorId;

        this.ingredients.AddRange( ingredients );
        this.steps.AddRange( steps );
        this.tags.AddRange( tags ?? [] );
    }

    public string Name { get; private set; }

    public int TimeToCook { get; private set; }

    public int CountPersons { get; private set; }

    public string Description { get; private set; }

    public string ImagePath { get; private set; }

    public Guid AuthorId { get; private set; } // внешний ключ

    public IReadOnlyCollection<Ingredient> Ingredients => this.ingredients.AsReadOnly();

    public IReadOnlyCollection<Step> Steps => this.steps.AsReadOnly();

    public IReadOnlyCollection<Tag> Tags => this.tags.AsReadOnly();

    public void Update(
        string name,
        string description,
        int timeToCook,
        int countPersons,
        string imagePath,
        IEnumerable<Ingredient>? ingredients = null,
        IEnumerable<Step>? steps = null,
        IEnumerable<Tag>? tags = null )
    {
        this.Validate(
            name,
            description,
            timeToCook,
            countPersons,
            imagePath );

        this.Name = name;
        this.Description = description;
        this.TimeToCook = timeToCook;
        this.CountPersons = countPersons;
        this.ImagePath = imagePath;

        this.ingredients.SynchronizeByContent( ingredients );
        this.steps.SynchronizeByContent( steps );
        this.tags.SynchronizeByContent( tags );
    }

    private void Validate(
        string name,
        string description,
        int timeToCook,
        int countPersons,
        string? imageUrl )
    {
        if (string.IsNullOrWhiteSpace( name ))
        {
            this.AddError( nameof( name ), "Название рецепта не может быть пустым" );
        }

        if (string.IsNullOrWhiteSpace( description ))
        {
            this.AddError( nameof( description ), "Описание не может быть пустым" );
        }

        if (timeToCook == 0)
        {
            this.AddError( nameof( timeToCook ), "Время приготовления блюда не может быть равным 0" );
        }

        if (countPersons == 0)
        {
            this.AddError( nameof( countPersons ), "Количество персон должно быть больше 0" );
        }

        if (!string.IsNullOrEmpty( imageUrl ) && !Uri.IsWellFormedUriString( imageUrl, UriKind.Absolute ))
        {
            this.AddError( nameof( imageUrl ), "Некорректный формат URL картинки" );
        }

        // Вызов единого метода вывода ошибок
        this.EnsureValid();
    }
}