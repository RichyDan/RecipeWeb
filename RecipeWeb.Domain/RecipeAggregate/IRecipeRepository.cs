namespace RecipeWeb.Domain.RecipeAggregate;

public interface IRecipeRepository
{
    Task<Recipe> GetByIdAsync(Guid id);
    Task<IEnumerable<Recipe>> GetAllAsync();
    Task AddAsync(Recipe recipe);
    void Update(Recipe recipe);
    Task DeleteAsync(Guid id);
}