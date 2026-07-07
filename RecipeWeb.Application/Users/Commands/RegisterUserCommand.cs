using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Users.Commands;

public record RegisterUserCommand(
    string firstName,
    string login,
    string password,
    string description): ICommand;