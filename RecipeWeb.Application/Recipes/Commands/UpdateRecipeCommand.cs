using RecipeWeb.Application.Common.DTOs;
using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Recipes.Commands;

public class UpdateRecipeCommand : ICommand
{
    public Guid RecipeId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int TimeToCook { get; set; }
    public int CountPersons { get; set; }
    public string ImagePath { get; set; } = null!;
    public List<IngredientDto>? Ingredients { get; set; }
    public List<StepDto>? Steps { get; set; }
    public List<TagDto>? Tags { get; set; }
}