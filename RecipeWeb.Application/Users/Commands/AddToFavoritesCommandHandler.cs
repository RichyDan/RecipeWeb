using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Domain.UserAggregate;

namespace RecipeWeb.Application.Users.Commands;

public class AddToFavoritesCommandHandler(IUserRepository userRepository) : ICommandHandler<AddToFavoritesCommand>
{
    public async Task Handle(AddToFavoritesCommand command, CancellationToken cancellationToken)
    {
        User user = await userRepository.GetByIdAsync(command.UserId)
            ?? throw new KeyNotFoundException($"Пользователь с Id {command.UserId} не найден");

        user.AddToFavorites(command.RecipeId);
    }
}