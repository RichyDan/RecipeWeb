using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Domain.RecipeAggregate;

namespace RecipeWeb.Application.Recipes.Commands;

public class DeleteRecipeCommandHandler( IRecipeRepository recipeRepository ) : ICommandHandler<DeleteRecipeCommand>
{
    public async Task Handle( DeleteRecipeCommand command, CancellationToken cancellationToken ) =>
        await recipeRepository.DeleteAsync( command.recipeId, cancellationToken );
}