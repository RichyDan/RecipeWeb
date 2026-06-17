using RecipeWeb.Application.Common.DTOs;
using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Recipes.Commands;

public record UpdateRecipeCommand : ICommand
{
    public Guid RecipeId { get; set; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public int TimeToCook { get; init; }
    public int CountPersons { get; init; }
    public string ImagePath { get; init; } = null!;
    public List<IngredientDto>? Ingredients { get; init; }
    public List<StepDto>? Steps { get; init; }
    public List<TagDto>? Tags { get; init; }
}