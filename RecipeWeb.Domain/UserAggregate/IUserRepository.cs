namespace RecipeWeb.Domain.UserAggregate;

public interface IUserRepository
{
    public Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    public Task<User> FindByFirstNameAsync(string firstname, CancellationToken cancellationToken = default);

    public Task<User> FindByLoginAsync(string login, CancellationToken cancellationToken = default);

    public Task AddAsync(User user, CancellationToken cancellationToken = default);

    public void Update(User user);

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
