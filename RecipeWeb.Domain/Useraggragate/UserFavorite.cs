namespace RecipeWeb.Domain.Useraggragate;

public class UserFavorite
{
    public UserFavorite(
        Guid userId,
        Guid recipeId
    ) : base()
    {
        UserId = userId;
        RecipeId = recipeId;
    }
    
    public Guid UserId { get; set; }
    public Guid RecipeId { get; set; }
}