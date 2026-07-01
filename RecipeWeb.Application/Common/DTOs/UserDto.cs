namespace RecipeWeb.Application.Common.DTOs;

public record UserDto(
    Guid Id,
    string FirstName,
    string Login,
    string Description,
    List<Guid> LikedRecipeIds,
    List<Guid> FavoriteRecipeIds
);