using RecipeWeb.Domain.Common;
namespace RecipeWeb.Domain.UserAggregate
{
    public class User : Entity
    {
        private readonly List<UserLike> _likedRecipes = [];
        private readonly List<UserFavorite> _favoriteRecipes = [];

        public User(
            string firstName,
            string login,
            string password,
            string description)
        {
            Validate(firstName, login, password);

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
            string firstName,
            string login,
            string password,
            string description)
        {
            Validate(firstName, login, password);

            FirstName = firstName;
            Login = login;
            Password = password;
            Description = description;
        }

        public void AddLike(Guid recipeId)
        {
            if (!_likedRecipes.Any(l => l.RecipeId == recipeId))
            {
                _likedRecipes.Add(new UserLike(Id, recipeId));
            }
        }

        public void RemoveLike(Guid recipeId)
        {
            UserLike? like = _likedRecipes.FirstOrDefault(l => l.RecipeId == recipeId);
            if (like != null)
            {
                _likedRecipes.Remove(like);
            }
        }

        public void AddToFavorites(Guid recipeId)
        {
            if (!_favoriteRecipes.Any(f => f.RecipeId != recipeId))
            {
                _favoriteRecipes.Add(new UserFavorite(Id, recipeId));
            }
        }

        public void RemoveFromFavorites(Guid recipeId)
        {
            UserFavorite? favorite = _favoriteRecipes.FirstOrDefault(f => f.RecipeId == recipeId);
            if (favorite != null)
            {
                _favoriteRecipes.Remove(favorite);
            }
        }

        private void Validate(string firstName, string login, string password)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                AddError(nameof(FirstName), "Имя не может быть пустым");

            if (string.IsNullOrWhiteSpace(login))
                AddError(nameof(login), "Логин не может быть пустым");
            else if (login.Length < 3)
                AddError(nameof(login), "Логин слишком короткий. Логин должен содержать минимум 3 символа");

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                AddError(nameof(password), "Пароль не должен быть пустым и должен содержать не менее 6 символов");

            // вызов единого метода для вывода ошибок валидации
            EnsureValid();
        }
    }
}