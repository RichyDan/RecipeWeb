using RecipeWeb.Application.Common.DTOs;
using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Domain.RecipeAggregate;

namespace RecipeWeb.Application.Recipes.Queries;

public class GetAllRecipesQueryHandler(IRecipeRepository recipeRepository) : IQueryHandler<GetAllRecipesQuery, List<RecipeDto>>
{
    public async Task<List<RecipeDto>> Handle(GetAllRecipesQuery query, CancellationToken cancellationToken)
    {
        IEnumerable<Recipe> recipes = await recipeRepository.GetAllAsync();

        return recipes.Select(recipe => new RecipeDto(
            recipe.Id,
            recipe.Name,
            recipe.Description,
            recipe.TimeToCook,
            recipe.CountPersons,
            recipe.ImagePath,
            recipe.AuthorId,
            recipe.Ingredients.Select(i => new IngredientDto(i.Name, i.Products.ToList())).ToList(),
            recipe.Steps.Select(s => new StepDto(s.Instructions)).ToList(),
            recipe.Tags.Select(t => new TagDto(t.Name)).ToList()
        )).ToList();
    }
}
