namespace RecipeWeb.Domain.Useraggrigate;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id);
    Task<User> FindByFirstNameAsync(string firstname);
    Task<User> FindByLoginAsync(string login);
    Task AddAsync(User user);
    Task DeleteAsync(User user); 
}   