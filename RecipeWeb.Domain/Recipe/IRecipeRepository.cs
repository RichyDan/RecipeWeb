namespace RecipeWeb.Domain.Recipe;

public interface IRecipeRepository
{
    Task<Recipe> GetByIdAsync(int id);
    Task<IEnumerable<Recipe>> GetAllAsync();
    Task AddAsync(Recipe recipe);
    Task DeleteAsync(int id);
}