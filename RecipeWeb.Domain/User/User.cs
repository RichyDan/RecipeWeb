using RecipeWeb.Domain.Common;
namespace RecipeWeb.Domain.User;

public class User : Entity
{
    private readonly List<UserLike> _likedRecipes = new();
    private readonly List<UserFavorite> _favoriteRecipes = new();
    
    public User (
        string firstName, 
        string login, 
        string password, 
        string description)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        Login = login;
        Password = password;
        Description = description;
    }
    
    public string FirstName { get; private set; }
    public string Login { get; private set; }
    public string Password { get; private set; }
    public string Description { get; private set; }

    public IReadOnlyCollection<UserLike> LikedRecipes => _likedRecipes.AsReadOnly();
    public IReadOnlyCollection<UserFavorite> FavoriteRecipes => _favoriteRecipes.AsReadOnly();
    
    public void Update(
        string firstname, 
        string login, 
        string password, 
        string description,
        IEnumerable<UserLike>? likedRecipes = null, 
        IEnumerable<UserFavorite>? favoriteRecipes = null)
    {
        FirstName = firstname;
        Login = login;
        Password = password;
        Description = description;
        
        if (likedRecipes != null)
            _likedRecipes.AddRange(likedRecipes);

        if (favoriteRecipes != null)
            _favoriteRecipes.AddRange(favoriteRecipes);
    }
    
    public void AddLike(Guid recipeId)
    {
        if (_likedRecipes.All(l => l.RecipeId != recipeId))
        {
            _likedRecipes.Add(new UserLike(this.Id, recipeId));
        }
    }

    public void RemoveLike(Guid recipeId)
    {
        var like = _likedRecipes.FirstOrDefault(l => l.RecipeId == recipeId);
        if (like != null)
        {
            _likedRecipes.Remove(like);
        }
    }

    public void AddToFavorites(Guid recipeId)
    {
        if (_favoriteRecipes.All(f => f.RecipeId != recipeId))
        {
            _favoriteRecipes.Add(new UserFavorite(this.Id, recipeId));
        }
    }

    public void RemoveFromFavorites(Guid recipeId)
    {
        var favorite = _favoriteRecipes.FirstOrDefault(f => f.RecipeId == recipeId);
        if (favorite != null)
        {
            _favoriteRecipes.Remove(favorite);
        }
    }
}