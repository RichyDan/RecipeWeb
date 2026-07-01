using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Users.Commands;

public record LikeRecipeCommand(Guid UserId, Guid RecipeId) : ICommand;