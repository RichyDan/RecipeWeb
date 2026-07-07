using RecipeWeb.Application.Common.DTOs;
using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Recipes.Commands;

public record UpdateRecipeCommand(
    Guid recipeId,
    string name,
    string description,
    int timeToCook,
    int countPersons,
    string imagePath,
    List<IngredientDto>? ingredients,
    List<StepDto>? steps,
    List<TagDto>? tags): ICommand;