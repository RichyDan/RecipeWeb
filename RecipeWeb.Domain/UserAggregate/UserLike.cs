namespace RecipeWeb.Domain.UserAggregate;

public class UserLike(Guid userId, Guid recipeId)
{
    public Guid UserId { get; set; } = userId;

    public Guid RecipeId { get; set; } = recipeId;
}
