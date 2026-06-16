using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Users.Commands;

public class DeleteUserCommand : ICommand
{
    public Guid UserId { get; set; }
}