using Microsoft.EntityFrameworkCore;
using RecipeWeb.Domain.RecipeAggregate;
using RecipeWeb.Infrastructure.Persistence;

namespace RecipeWeb.Infrastructure.Repositories;

public class RecipeRepository(RecipeDbContext context) : IRecipeRepository
{
    /// <inheritdoc/>
    public async Task<Recipe> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await AddIncludes(context.Recipes)
            .SingleOrDefaultAsync(r => r.Id == id, cancellationToken);

    /// <inheritdoc/>
    public async Task<IEnumerable<Recipe>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await AddIncludes(context.Recipes)
            .ToListAsync(cancellationToken);

    /// <inheritdoc/>
    public async Task AddAsync(Recipe recipe, CancellationToken cancellationToken = default) =>
        await context.Recipes.AddAsync(recipe, cancellationToken);

    /// <inheritdoc/>
    public void Update(Recipe recipe) => context.Recipes.Update(recipe);

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) =>
        await context.Recipes
            .Where(r => r.Id == id)
            .ExecuteDeleteAsync(cancellationToken);

    private static IQueryable<Recipe> AddIncludes(IQueryable<Recipe> query) =>
        query
            .Include(r => r.Ingredients)
            .Include(r => r.Steps)
            .Include(r => r.Tags);
}
