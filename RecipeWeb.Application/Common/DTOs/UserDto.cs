namespace RecipeWeb.Application.Common.DTOs;

public record UserDto(
    Guid id,
    string firstName,
    string login,
    string description,
    List<Guid> likedRecipeIds,
    List<Guid> favoriteRecipeIds );