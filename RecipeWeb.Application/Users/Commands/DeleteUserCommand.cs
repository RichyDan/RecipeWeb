using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Users.Commands;

public record DeleteUserCommand(Guid userId): ICommand;