namespace RecipeWeb.Domain.Recipe;

public interface IRecipeRepository
{
    Task<User.User> GetByIdAsync(int id);
    Task<User.User> FindByFirstNameAsync(string firstname);
    Task<User.User> FindByLoginAsync(string login);
    Task AddAsync(User.User user);
    Task UpdateAsync(User.User user);
    Task DeleteAsync(User.User user);
}   