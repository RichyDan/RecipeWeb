using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Users.Commands;

public record RegisterUserCommand(
    string FirstName,
    string Login,
    string Password,
    string Description
) : ICommand;