using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Users.Commands;

public record RemoveFromFavoritesCommand( Guid userId, Guid recipeId ) : ICommand;