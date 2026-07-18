using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Users.Commands;

public record AddToFavoritesCommand( Guid userId, Guid recipeId ) : ICommand;