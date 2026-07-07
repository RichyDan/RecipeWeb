using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Domain.RecipeAggregate;
using RecipeWeb.Domain.UserAggregate;

namespace RecipeWeb.Application.Users.Commands;

public class RemoveFromFavoritesCommandHandler(IUserRepository userRepository, IRecipeRepository recipeRepository) : ICommandHandler<RemoveFromFavoritesCommand>
{
    public async Task Handle(RemoveFromFavoritesCommand command, CancellationToken cancellationToken)
    {
        User user = await userRepository.GetByIdAsync(command.UserId, cancellationToken)
            ?? throw new InvalidOperationException($"Пользователь с Id {command.UserId} не найден");

        if (await recipeRepository.GetByIdAsync(command.RecipeId, cancellationToken) is null)
            throw new InvalidOperationException($"Рецепт с Id {command.RecipeId} не найден");

        user.RemoveFromFavorites(command.RecipeId);
    }
}