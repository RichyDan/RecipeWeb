using Microsoft.EntityFrameworkCore;
using RecipeWeb.Domain.RecipeAggregate;
using RecipeWeb.Infrastructure.Persistence;

namespace RecipeWeb.Infrastructure.Repositories;

public class RecipeRepository(RecipeDbContext context): IRecipeRepository
{
    public async Task<Recipe> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await this.AddIncludes(context.Recipes)
            .SingleOrDefaultAsync(r => r.Id == id, cancellationToken);

    public async Task<IEnumerable<Recipe>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await this.AddIncludes(context.Recipes)
            .ToListAsync(cancellationToken);

    public async Task AddAsync(Recipe recipe, CancellationToken cancellationToken = default) =>
        await context.Recipes.AddAsync(recipe, cancellationToken);

    public void Update(Recipe recipe) => context.Recipes.Update(recipe);

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) =>
        await context.Recipes
            .Where(r => r.Id == id)
            .ExecuteDeleteAsync(cancellationToken);

    private IQueryable<Recipe> AddIncludes(IQueryable<Recipe> query) =>
        query
            .Include(r => r.Ingredients)
            .Include(r => r.Steps)
            .Include(r => r.Tags);
}
