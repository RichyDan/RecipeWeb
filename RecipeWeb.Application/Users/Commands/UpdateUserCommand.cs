using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Users.Commands;

public class UpdateUserCommand : ICommand
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Description { get; set; } = null!;
}