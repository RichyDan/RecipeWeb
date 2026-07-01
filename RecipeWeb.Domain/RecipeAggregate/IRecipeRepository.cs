namespace RecipeWeb.Domain.RecipeAggregate;

public interface IRecipeRepository
{
    Task<Recipe> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Recipe>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Recipe recipe, CancellationToken cancellationToken = default);
    void Update(Recipe recipe);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}