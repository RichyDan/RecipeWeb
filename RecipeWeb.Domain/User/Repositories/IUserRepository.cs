namespace RecipeWeb.Domain.User.Repositories;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
    Task<User> GetByFirstNameAsync(string firstname);
    Task<User> GetByLoginAsync(string login);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
    Task<bool> ExistsByLoginAsync(string login);
    Task<bool> ExistsByFirstNameAsync(string firstname);
    Task<IEnumerable<User>> GetAllAsync();
}   