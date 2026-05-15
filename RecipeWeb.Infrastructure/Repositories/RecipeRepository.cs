using Microsoft.EntityFrameworkCore;
using RecipeWeb.Domain.RecipeAggregate;
using RecipeWeb.Infrastructure.Persistence;

namespace RecipeWeb.Infrastructure.Repositories;

public class RecipeRepository(RecipeDbContext context) : IRecipeRepository
{
    public async Task<Recipe> GetByIdAsync(Guid id)
    {
        return await AddIncludes(context.Recipes)
            .SingleOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Recipe>> GetAllAsync()
    {
        return await AddIncludes(context.Recipes)
            .ToListAsync();
    }

    public async Task AddAsync(Recipe recipe)
    {
        await context.Recipes.AddAsync(recipe);
    }

    public Task UpdateAsync(Recipe recipe)
    {
        context.Recipes.Update(recipe);

        // Не вызываем SaveChanges, это сделает UoW
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        await context.Recipes
            .Where(r => r.Id == id)
            .ExecuteDeleteAsync();
    }

    private IQueryable<Recipe> AddIncludes(IQueryable<Recipe> query)
    {
        return query
            .Include(r => r.Ingredients)
            .Include(r => r.Steps)
            .Include(r => r.Tags);
    }
}
