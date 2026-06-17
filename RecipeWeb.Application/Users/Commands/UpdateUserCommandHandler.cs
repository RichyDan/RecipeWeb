using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Domain.UserAggregate;

namespace RecipeWeb.Application.Users.Commands;

public class UpdateUserCommandHandler(IUserRepository userRepository) : ICommandHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        User user = await userRepository.GetByIdAsync(command.UserId)
            ?? throw new KeyNotFoundException($"Пользователь с Id {command.UserId} не найден");

        User existingByLogin = await userRepository.FindByLoginAsync(command.Login);
        if (existingByLogin != null && existingByLogin.Id != command.UserId)
            throw new InvalidOperationException($"Логин '{command.Login}' уже занят");

        user.Update(
            command.FirstName,
            command.Login,
            command.Password,
            command.Description);
    }
}