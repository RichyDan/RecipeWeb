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

    private static RecipeDto MapToDto(Recipe recipe) => new()
    {
        Id = recipe.Id,
        Name = recipe.Name,
        Description = recipe.Description,
        TimeToCook = recipe.TimeToCook,
        CountPersons = recipe.CountPersons,
        ImagePath = recipe.ImagePath,
        AuthorId = recipe.AuthorId,
        Ingredients = recipe.Ingredients.Select(i => new IngredientDto
        {
            Name = i.Name,
            Products = i.Products.ToList()
        }).ToList(),
        Steps = recipe.Steps.Select(s => new StepDto
        {
            Instructions = s.Instructions
        }).ToList(),
        Tags = recipe.Tags.Select(t => new TagDto
        {
            Name = t.Name
        }).ToList()
    };
}