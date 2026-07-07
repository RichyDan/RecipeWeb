using RecipeWeb.Application.Common.DTOs;
using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Recipes.Commands;

public record UpdateRecipeCommand(
    Guid RecipeId,
    string Name,
    string Description,
    int TimeToCook,
    int CountPersons,
    string ImagePath,
    List<IngredientDto>? Ingredients,
    List<StepDto>? Steps,
    List<TagDto>? Tags
) : ICommand;