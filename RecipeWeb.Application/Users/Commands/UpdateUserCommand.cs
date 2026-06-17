using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Users.Commands;

public record UpdateUserCommand : ICommand
{
    public Guid UserId { get; set; }
    public string FirstName { get; init; } = null!;
    public string Login { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string Description { get; init; } = null!;
}