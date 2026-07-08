using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Domain.RecipeAggregate;

namespace RecipeWeb.Application.Recipes.Commands;

public class CreateRecipeCommandHandler( IRecipeRepository recipeRepository ) : ICommandHandler<CreateRecipeCommand>
{
    public async Task Handle( CreateRecipeCommand command, CancellationToken cancellationToken )
    {
        var ingredients = command.ingredients
            .Select( i => new Ingredient( i.name, i.products ) )
            .ToList();

        var steps = command.steps
            .Select( s => new Step( s.instructions ) )
            .ToList();

        var tags = command.tags
            .Select( t => new Tag( t.name ) )
            .ToList();

        var recipe = new Recipe(
            command.name,
            command.description,
            command.timeToCook,
            command.countPersons,
            command.imagePath,
            command.authorId,
            ingredients,
            steps,
            tags );

        await recipeRepository.AddAsync( recipe, cancellationToken );
    }
}