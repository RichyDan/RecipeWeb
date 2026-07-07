using RecipeWeb.Domain.Common;

namespace RecipeWeb.Domain.UserAggregate;

public class User : Entity
{
    private readonly List<UserLike> likedRecipes =[];
    private readonly List<UserFavorite> favoriteRecipes =[];

    private User()
    {
    }

    public User(
        string firstName,
        string login,
        string password,
        string description)
    {
        this.Validate(firstName, login, password);

        this.FirstName = firstName;
        this.Login = login;
        this.Password = password;
        this.Description = description;
    }

    public string FirstName { get; private set; }

    public string Login { get; private set; }

    public string Password { get; private set; }

    public string Description { get; private set; }

    public IReadOnlyCollection<UserLike> LikedRecipes => this.likedRecipes.AsReadOnly();

    public IReadOnlyCollection<UserFavorite> FavoriteRecipes => this.favoriteRecipes.AsReadOnly();

    public void Update(
        string firstName,
        string login,
        string password,
        string description)
    {
        this.Validate(firstName, login, password);

        this.FirstName = firstName;
        this.Login = login;
        this.Password = password;
        this.Description = description;
    }

    public void AddLike(Guid recipeId)
    {
        if (!this.likedRecipes.Any(l => l.RecipeId == recipeId))
        {
            this.likedRecipes.Add(new UserLike(this.Id, recipeId));
        }
    }

    public void RemoveLike(Guid recipeId)
    {
        UserLike? like = this.likedRecipes.FirstOrDefault(l => l.RecipeId == recipeId);
        if (like != null)
        {
            this.likedRecipes.Remove(like);
        }
    }

    public void AddToFavorites(Guid recipeId)
    {
        if (!this.favoriteRecipes.Any(f => f.RecipeId != recipeId))
        {
            this.favoriteRecipes.Add(new UserFavorite(this.Id, recipeId));
        }
    }

    public void RemoveFromFavorites(Guid recipeId)
    {
        UserFavorite? favorite = this.favoriteRecipes.FirstOrDefault(f => f.RecipeId == recipeId);
        if (favorite != null)
        {
            this.favoriteRecipes.Remove(favorite);
        }
    }

    private void Validate(string firstName, string login, string password)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            this.AddError(nameof(this.FirstName), "Имя не может быть пустым");
        }

        if (string.IsNullOrWhiteSpace(login))
        {
            this.AddError(nameof(login), "Логин не может быть пустым");
        }
        else if (login.Length < 3)
        {
            this.AddError(nameof(login), "Логин слишком короткий. Логин должен содержать минимум 3 символа");
        }

        if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
        {
            this.AddError(nameof(password), "Пароль не должен быть пустым и должен содержать не менее 6 символов");
        }

        // вызов единого метода для вывода ошибок валидации
        this.EnsureValid();
    }
}