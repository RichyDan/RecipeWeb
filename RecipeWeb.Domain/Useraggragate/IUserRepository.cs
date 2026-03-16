namespace RecipeWeb.Domain.Useraggragate;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
    Task<User> FindByFirstNameAsync(string firstname);
    Task<User> FindByLoginAsync(string login);
    Task AddAsync(User user);
    Task DeleteAsync(User user);
}   