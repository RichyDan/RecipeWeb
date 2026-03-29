namespace RecipeWeb.Domain.Useraggragate;

public class UserLike
{
    public UserLike(Guid userId, Guid recipeId) : base()
    {
        UserId = userId;
        RecipeId = recipeId;
    }

    public Guid UserId { get; set; }
    public Guid RecipeId { get; set; }
}