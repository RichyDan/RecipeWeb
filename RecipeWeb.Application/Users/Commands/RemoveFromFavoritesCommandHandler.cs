using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Domain.UserAggregate;

namespace RecipeWeb.Application.Users.Commands;

public class RemoveFromFavoritesCommandHandler(IUserRepository userRepository) : ICommandHandler<RemoveFromFavoritesCommand>
{
    public async Task Handle(RemoveFromFavoritesCommand command, CancellationToken cancellationToken)
    {
        User user = await userRepository.GetByIdAsync(command.UserId)
            ?? throw new KeyNotFoundException($"Пользователь с Id {command.UserId} не найден");

        user.RemoveFromFavorites(command.RecipeId);
    }
}