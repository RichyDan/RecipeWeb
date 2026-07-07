namespace RecipeWeb.Domain.UserAggregate;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User> FindByFirstNameAsync(string firstname, CancellationToken cancellationToken = default);
    Task<User> FindByLoginAsync(string login, CancellationToken cancellationToken = default);
    Task AddAsync(User user, CancellationToken cancellationToken = default);
    void Update(User user);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}