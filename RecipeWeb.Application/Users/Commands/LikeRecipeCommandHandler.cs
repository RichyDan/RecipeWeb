using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Domain.UserAggregate;

namespace RecipeWeb.Application.Users.Commands;

public class LikeRecipeCommandHandler(IUserRepository userRepository) : ICommandHandler<LikeRecipeCommand>
{
    public async Task Handle(LikeRecipeCommand command, CancellationToken cancellationToken)
    {
        User user = await userRepository.GetByIdAsync(command.UserId) ??
            throw new KeyNotFoundException($"Пользователь с Id {command.UserId} не найден");

        user.AddLike(command.RecipeId);
    }
}