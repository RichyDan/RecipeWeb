using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Domain.UserAggregate;

namespace RecipeWeb.Application.Users.Commands;

public class DeleteUserCommandHandler(IUserRepository userRepository) : ICommandHandler<DeleteUserCommand>
{
    /// <inheritdoc/>
    public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken) =>
        await userRepository.DeleteAsync(command.userId, cancellationToken);
}
