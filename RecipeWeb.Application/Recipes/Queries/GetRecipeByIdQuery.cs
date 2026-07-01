using RecipeWeb.Application.Common.DTOs;
using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Recipes.Queries;

public record GetRecipeByIdQuery(Guid RecipeId) : IQuery<RecipeDto?>;