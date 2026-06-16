using RecipeWeb.Application.Common.DTOs;
using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Recipes.Queries;

public class GetRecipeByIdQuery : IQuery<RecipeDto?>
{
    public Guid RecipeId { get; set; }
}