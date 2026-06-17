using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Users.Commands;

public record UnlikeRecipeCommand(Guid UserId, Guid RecipeId) : ICommand;