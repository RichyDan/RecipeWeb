namespace RecipeWeb.Application.Common.DTOs;

public record RecipeDto(
    Guid id,
    string name,
    string description,
    int timeToCook,
    int countPersons,
    string imagePath,
    Guid authorId,
    List<IngredientDto> ingredients,
    List<StepDto> steps,
    List<TagDto> tags );
