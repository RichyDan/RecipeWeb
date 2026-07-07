namespace RecipeWeb.Domain.RecipeAggregate;

public interface IRecipeRepository
{
    public Task<Recipe> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    public Task<IEnumerable<Recipe>> GetAllAsync(CancellationToken cancellationToken = default);

    public Task AddAsync(Recipe recipe, CancellationToken cancellationToken = default);

    public void Update(Recipe recipe);

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
