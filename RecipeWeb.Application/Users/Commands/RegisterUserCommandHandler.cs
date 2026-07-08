using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Domain.UserAggregate;

namespace RecipeWeb.Application.Users.Commands;

public class RegisterUserCommandHandler( IUserRepository userRepository, IPasswordHasher passwordHasher ) : ICommandHandler<RegisterUserCommand>
{
    public async Task Handle( RegisterUserCommand command, CancellationToken cancellationToken )
    {
        User existing = await userRepository.FindByLoginAsync( command.login, cancellationToken );

        if (existing != null)
        {
            throw new InvalidOperationException( $"Пользователь с логином '{command.login}' уже существует" );
        }

        var user = new User(
            command.firstName,
            command.login,
            passwordHasher.Hash( command.password ),
            command.description );

        await userRepository.AddAsync( user, cancellationToken );
    }
}