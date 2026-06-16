using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Domain.UserAggregate;

namespace RecipeWeb.Application.Users.Commands;

public class UnlikeRecipeCommandHandler(IUserRepository userRepository) : ICommandHandler<UnlikeRecipeCommand>
{
    public async Task Handle(UnlikeRecipeCommand command, CancellationToken cancellationToken)
    {
        User user = await userRepository.GetByIdAsync(command.UserId)
            ?? throw new KeyNotFoundException($"Пользователь с Id {command.UserId} не найден");

        user.RemoveLike(command.RecipeId);
    }
}