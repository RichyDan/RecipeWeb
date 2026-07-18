using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Domain.RecipeAggregate;
using RecipeWeb.Domain.UserAggregate;

namespace RecipeWeb.Application.Users.Commands;

public class RemoveFromFavoritesCommandHandler( IUserRepository userRepository, IRecipeRepository recipeRepository ) : ICommandHandler<RemoveFromFavoritesCommand>
{
    public async Task Handle( RemoveFromFavoritesCommand command, CancellationToken cancellationToken )
    {
        User user = await userRepository.GetByIdAsync( command.userId, cancellationToken )
            ?? throw new InvalidOperationException( $"Пользователь с Id {command.userId} не найден" );

        if (await recipeRepository.GetByIdAsync( command.recipeId, cancellationToken ) is null)
        {
            throw new InvalidOperationException( $"Рецепт с Id {command.recipeId} не найден" );
        }

        user.RemoveFromFavorites( command.recipeId );
    }
}