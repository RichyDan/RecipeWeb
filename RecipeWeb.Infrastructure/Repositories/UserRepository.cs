using Microsoft.EntityFrameworkCore;
using RecipeWeb.Domain.RecipeAggregate;
using RecipeWeb.Domain.UserAggregate;
using RecipeWeb.Infrastructure.Persistence;

namespace RecipeWeb.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RecipeDbContext _context;

        public UserRepository(RecipeDbContext context) => _context = context;

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .Include(u => u.FavoriteRecipes)
                .Include(u => u.LikedRecipes)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> FindByFirstNameAsync(string firstname)
        {
            return await _context.Users
                .Include(u => u.LikedRecipes)
                .Include(u => u.FavoriteRecipes)
                .FirstOrDefaultAsync(u => u.FirstName == firstname);
        }

        public async Task<User> FindByLoginAsync(string login)
        {
            return await _context.Users
                .Include(u => u.LikedRecipes)
                .Include(u => u.FavoriteRecipes)
                .FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }
        public Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            return Task.CompletedTask;
        }
    }
}
