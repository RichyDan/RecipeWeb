using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Domain.RecipeAggregate;
using RecipeWeb.Domain.UserAggregate;

namespace RecipeWeb.Application.Users.Commands;

public class UnlikeRecipeCommandHandler(IUserRepository userRepository, IRecipeRepository recipeRepository) : ICommandHandler<UnlikeRecipeCommand>
{
    public async Task Handle(UnlikeRecipeCommand command, CancellationToken cancellationToken)
    {
        User user = await userRepository.GetByIdAsync(command.UserId, cancellationToken)
            ?? throw new InvalidOperationException($"Пользователь с Id {command.UserId} не найден");

        if (await recipeRepository.GetByIdAsync(command.RecipeId, cancellationToken) is null)
            throw new InvalidOperationException($"Рецепт с Id {command.RecipeId} не найден");

        user.RemoveLike(command.RecipeId);
    }
}