namespace RecipeWeb.Application.Common.DTOs;

public class RecipeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int TimeToCook { get; set; }
    public int CountPersons { get; set; }
    public string ImagePath { get; set; } = null!;
    public Guid AuthorId { get; set; }
    public List<IngredientDto> Ingredients { get; set; } = [];
    public List<StepDto> Steps { get; set; } = [];
    public List<TagDto> Tags { get; set; } = [];
}
