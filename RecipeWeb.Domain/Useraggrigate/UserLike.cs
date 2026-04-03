namespace RecipeWeb.Domain.Useraggrigate;

public class UserLike
{
    public UserLike(Guid userId, Guid recipeId)
    {
        UserId = userId;
        RecipeId = recipeId;
    }

    public Guid UserId { get; set; }
    public Guid RecipeId { get; set; }
}