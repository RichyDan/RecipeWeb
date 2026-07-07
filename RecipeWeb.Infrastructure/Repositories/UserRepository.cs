using Microsoft.EntityFrameworkCore;
using RecipeWeb.Domain.UserAggregate;
using RecipeWeb.Infrastructure.Persistence;

namespace RecipeWeb.Infrastructure.Repositories;

public class UserRepository(RecipeDbContext context): IUserRepository
{
    public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await this.AddIncludes(context.Users)
            .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);

    public async Task<User> FindByFirstNameAsync(string firstname, CancellationToken cancellationToken = default) =>
        await this.AddIncludes(context.Users)
            .SingleOrDefaultAsync(u => u.FirstName == firstname, cancellationToken);

    public async Task<User> FindByLoginAsync(string login, CancellationToken cancellationToken = default) =>
        await this.AddIncludes(context.Users)
            .SingleOrDefaultAsync(u => u.Login == login, cancellationToken);

    public async Task AddAsync(User user, CancellationToken cancellationToken = default) =>
        await context.Users.AddAsync(user, cancellationToken);

    public void Update(User user) => context.Users.Update(user);

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) =>
        await context.Users
            .Where(r => r.Id == id)
            .ExecuteDeleteAsync(cancellationToken);

    private IQueryable<User> AddIncludes(IQueryable<User> query) =>
        query
            .Include(u => u.LikedRecipes)
            .Include(u => u.FavoriteRecipes);
}
