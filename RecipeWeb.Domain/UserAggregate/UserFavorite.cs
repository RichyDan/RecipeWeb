namespace RecipeWeb.Domain.UserAggregate;

public class UserFavorite
{
    public UserFavorite(Guid userId, Guid recipeId)
    {
        UserId = userId;
        RecipeId = recipeId;
    }

    public Guid UserId { get; set; }
    public Guid RecipeId { get; set; }
}