using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Domain.UserAggregate;

namespace RecipeWeb.Application.Users.Commands;

public class RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher) : ICommandHandler<RegisterUserCommand>
{
    public async Task Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        User existing = await userRepository.FindByLoginAsync(command.Login, cancellationToken);

        if (existing != null)
            throw new InvalidOperationException($"Пользователь с логином '{command.Login}' уже существует");

        var user = new User(
            command.FirstName,
            command.Login,
            passwordHasher.Hash(command.Password),
            command.Description);

        await userRepository.AddAsync(user, cancellationToken);
    }
}