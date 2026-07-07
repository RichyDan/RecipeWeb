using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Domain.UserAggregate;

namespace RecipeWeb.Application.Users.Commands;

public class UpdateUserCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher)
    : ICommandHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        User user = await userRepository.GetByIdAsync(command.userId, cancellationToken)
            ?? throw new InvalidOperationException($"Пользователь с Id {command.userId} не найден");

        User existingByLogin = await userRepository.FindByLoginAsync(command.login, cancellationToken);
        if (existingByLogin != null && existingByLogin.Id != command.userId)
        {
            throw new InvalidOperationException($"Логин '{command.login}' уже занят");
        }

        user.Update(
            command.firstName,
            command.login,
            passwordHasher.Hash(command.password),
            command.description);
    }
}