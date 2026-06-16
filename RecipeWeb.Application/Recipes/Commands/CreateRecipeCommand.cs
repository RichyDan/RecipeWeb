using RecipeWeb.Application.Common.DTOs;
using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Recipes.Commands;

public class CreateRecipeCommand : ICommand
{
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