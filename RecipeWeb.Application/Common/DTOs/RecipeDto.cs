namespace RecipeWeb.Application.Common.DTOs;

public record RecipeDto(
    Guid Id,
    string Name,
    string Description,
    int TimeToCook,
    int CountPersons,
    string ImagePath,
    Guid AuthorId,
    List<IngredientDto> Ingredients,
    List<StepDto> Steps,
    List<TagDto> Tags
);
