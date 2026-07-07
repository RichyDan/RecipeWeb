namespace RecipeWeb.Domain.UserAggregate;

public class UserLike
{
    public UserLike(Guid userId, Guid recipeId)
    {
        this.UserId = userId;
        this.RecipeId = recipeId;
    }

    public Guid UserId { get; set; }

    public Guid RecipeId { get; set; }
}