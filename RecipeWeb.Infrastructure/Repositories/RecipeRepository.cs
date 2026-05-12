using Microsoft.EntityFrameworkCore;
using RecipeWeb.Domain.RecipeAggregate;
using RecipeWeb.Infrastructure.Persistence;

namespace RecipeWeb.Infrastructure.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly RecipeDbContext _context;

        public RecipeRepository(RecipeDbContext context) => _context = context;

        public async Task<Recipe> GetByIdAsync(Guid id)
        {
            return await _context.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.Steps)
                .Include(r => r.Tags)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            return await _context.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.Steps)
                .Include(r => r.Tags)
                .ToListAsync();
        }

        public async Task AddAsync(Recipe recipe)
        {
            await _context.Recipes.AddAsync(recipe);
        }

        public Task DeleteAsync(Guid id)
        {
            var recipe = _context.Recipes.FirstOrDefault(r => r.Id == id);
            if (recipe != null)
                _context.Recipes.Remove(recipe);
            return Task.CompletedTask;
        }
    }
}
