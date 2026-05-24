namespace RecipeWeb.Domain.UserAggregate;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id);
    Task<User> FindByFirstNameAsync(string firstname);
    Task<User> FindByLoginAsync(string login);
    Task AddAsync(User user);
    void Update(User user);
    Task DeleteAsync(Guid id);
}