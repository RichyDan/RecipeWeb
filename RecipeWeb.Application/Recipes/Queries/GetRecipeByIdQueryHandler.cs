using RecipeWeb.Application.Common.DTOs;
using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Domain.RecipeAggregate;

namespace RecipeWeb.Application.Recipes.Queries;

public class GetRecipeByIdQueryHandler(IRecipeRepository recipeRepository) : IQueryHandler<GetRecipeByIdQuery, RecipeDto?>
{
    public async Task<RecipeDto?> Handle(GetRecipeByIdQuery query, CancellationToken cancellationToken)
    {
        Recipe? recipe = await recipeRepository.GetByIdAsync(query.RecipeId);

        return recipe is null ? null : MapToDto(recipe);
    }

    private static RecipeDto MapToDto(Recipe recipe) => 
        new(
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
        );
}