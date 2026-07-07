using Microsoft.EntityFrameworkCore;
using RecipeWeb.Application.Common.DTOs;
using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Application.Recipes.Queries;

namespace RecipeWeb.Infrastructure.Persistence.QueryHandlers.Recipes;

public class GetAllRecipesQueryHandler(RecipeDbContext context) : IQueryHandler<GetAllRecipesQuery, List<RecipeDto>>
{
    /// <inheritdoc/>
    public async Task<List<RecipeDto>> Handle(GetAllRecipesQuery query, CancellationToken cancellationToken) =>
        await context.Recipes
            .AsNoTracking()
            .Select(recipe => new RecipeDto(
            recipe.Id,
            recipe.Name,
            recipe.Description,
            recipe.TimeToCook,
            recipe.CountPersons,
            recipe.ImagePath,
            recipe.AuthorId,
            recipe.Ingredients.Select(i => new IngredientDto(i.Name, i.Products.ToList())).ToList(),
            recipe.Steps.Select(s => new StepDto(s.Instructions)).ToList(),
            recipe.Tags.Select(t => new TagDto(t.Name)).ToList()))
            .ToListAsync(cancellationToken);
}
