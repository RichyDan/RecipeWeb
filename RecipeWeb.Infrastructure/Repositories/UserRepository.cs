using Microsoft.EntityFrameworkCore;
using RecipeWeb.Domain.UserAggregate;
using RecipeWeb.Infrastructure.Persistence;

namespace RecipeWeb.Infrastructure.Repositories;

public class UserRepository(RecipeDbContext context) : IUserRepository
{
    /// <inheritdoc/>
    public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await AddIncludes(context.Users)
            .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);

    /// <inheritdoc/>
    public async Task<User> FindByFirstNameAsync(string firstname, CancellationToken cancellationToken = default) =>
        await AddIncludes(context.Users)
            .SingleOrDefaultAsync(u => u.FirstName == firstname, cancellationToken);

    /// <inheritdoc/>
    public async Task<User> FindByLoginAsync(string login, CancellationToken cancellationToken = default) =>
        await AddIncludes(context.Users)
            .SingleOrDefaultAsync(u => u.Login == login, cancellationToken);

    /// <inheritdoc/>
    public async Task AddAsync(User user, CancellationToken cancellationToken = default) =>
        await context.Users.AddAsync(user, cancellationToken);

    /// <inheritdoc/>
    public void Update(User user) => context.Users.Update(user);

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) =>
        await context.Users
            .Where(r => r.Id == id)
            .ExecuteDeleteAsync(cancellationToken);

    private static IQueryable<User> AddIncludes(IQueryable<User> query) =>
        query
            .Include(u => u.LikedRecipes)
            .Include(u => u.FavoriteRecipes);
}
