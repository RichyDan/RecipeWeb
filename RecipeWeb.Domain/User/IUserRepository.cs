namespace RecipeWeb.Domain.User;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
    Task<User> GetByFirstNameAsync(string firstname);
    Task<User> GetByLoginAsync(string login);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
    Task<IEnumerable<User>> GetAllAsync();
}   