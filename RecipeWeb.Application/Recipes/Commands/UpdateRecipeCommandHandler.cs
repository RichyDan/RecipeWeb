using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Domain.RecipeAggregate;

namespace RecipeWeb.Application.Recipes.Commands;

public class UpdateRecipeCommandHandler( IRecipeRepository recipeRepository ) : ICommandHandler<UpdateRecipeCommand>
{
    public async Task Handle( UpdateRecipeCommand command, CancellationToken cancellationToken )
    {
        Recipe recipe = await recipeRepository.GetByIdAsync( command.recipeId ) ??
            throw new InvalidOperationException( $"Рецепт с Id {command.recipeId} не найден" );

        var ingredients = command.ingredients?
            .Select( i => new Ingredient( i.name, i.products ) )
            .ToList();

        var steps = command.steps?
            .Select( s => new Step( s.instructions ) )
            .ToList();

        var tags = command.tags?
            .Select( t => new Tag( t.name ) )
            .ToList();

        recipe.Update(
            command.name,
            command.description,
            command.timeToCook,
            command.countPersons,
            command.imagePath,
            ingredients,
            steps,
            tags );
    }
}