using RecipeWeb.Application.Common.DTOs;
using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Domain.RecipeAggregate;

namespace RecipeWeb.Application.Recipes.Queries;

public class GetAllRecipesQueryHandler(IRecipeRepository recipeRepository) : IQueryHandler<GetAllRecipesQuery, List<RecipeDto>>
{
    public async Task<List<RecipeDto>> Handle(GetAllRecipesQuery query, CancellationToken cancellationToken)
    {
        IEnumerable<Recipe> recipes = await recipeRepository.GetAllAsync();

        return recipes.Select(recipe => new RecipeDto
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
        }).ToList();
    }
}