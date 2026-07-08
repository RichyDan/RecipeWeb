using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Recipes.Commands;

public record DeleteRecipeCommand( Guid recipeId ) : ICommand;