namespace RecipeWeb.Domain.Recipeaggrigate;

public interface IRecipeRepository
{
    Task<Recipe> GetByIdAsync(Guid id);
    Task<IEnumerable<Recipe>> GetAllAsync();
    Task AddAsync(Recipe recipe);
    Task DeleteAsync(Guid id);
}