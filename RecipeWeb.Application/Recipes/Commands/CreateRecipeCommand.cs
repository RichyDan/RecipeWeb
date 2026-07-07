using RecipeWeb.Application.Common.DTOs;
using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Recipes.Commands;

public record CreateRecipeCommand(
    string name,
    string description,
    int timeToCook,
    int countPersons,
    string imagePath,
    Guid authorId,
    List<IngredientDto> ingredients,
    List<StepDto> steps,
    List<TagDto> tags): ICommand;