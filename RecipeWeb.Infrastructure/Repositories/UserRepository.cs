using Microsoft.EntityFrameworkCore;
using RecipeWeb.Domain.UserAggregate;
using RecipeWeb.Infrastructure.Persistence;

namespace RecipeWeb.Infrastructure.Repositories;

public class UserRepository(RecipeDbContext context) : IUserRepository
{
    public async Task<User> GetByIdAsync(Guid id) =>
        await AddIncludes(context.Users)
            .SingleOrDefaultAsync(u => u.Id == id);

    public async Task<User> FindByFirstNameAsync(string firstname) =>
        await AddIncludes(context.Users)
            .SingleOrDefaultAsync(u => u.FirstName == firstname);

    public async Task<User> FindByLoginAsync(string login) =>
        await AddIncludes(context.Users)
            .SingleOrDefaultAsync(u => u.Login == login);

    public async Task AddAsync(User user) => await context.Users.AddAsync(user);

    public void Update(User user) => context.Users.Update(user);

    public async Task DeleteAsync(Guid id) =>
        await context.Users
            .Where(r => r.Id == id)
            .ExecuteDeleteAsync();

    private IQueryable<User> AddIncludes(IQueryable<User> query) =>
        query
            .Include(u => u.LikedRecipes)
            .Include(u => u.FavoriteRecipes);
}
