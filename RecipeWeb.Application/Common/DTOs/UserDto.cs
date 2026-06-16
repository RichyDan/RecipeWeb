namespace RecipeWeb.Application.Common.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<Guid> LikedRecipeIds { get; set; } = [];
    public List<Guid> FavoriteRecipeIds { get; set; } = [];
}