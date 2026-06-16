using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Domain.UserAggregate;

namespace RecipeWeb.Application.Users.Commands;

public class RegisterUserCommandHandler(IUserRepository userRepository) : ICommandHandler<RegisterUserCommand>
{
    public async Task Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var existing = await userRepository.FindByLoginAsync(command.Login);
        if (existing != null)
            throw new InvalidOperationException($"Пользователь с логином '{command.Login}' уже существует");

        var user = new User(
            command.FirstName,
            command.Login,
            command.Password,
            command.Description);

        await userRepository.AddAsync(user);
    }
}