using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Domain.RecipeAggregate;

namespace RecipeWeb.Application.Recipes.Commands;

public class CreateRecipeCommandHandler(IRecipeRepository recipeRepository) : ICommandHandler<CreateRecipeCommand>
{
    public async Task Handle(CreateRecipeCommand command, CancellationToken cancellationToken)
    {
        var ingredients = command.Ingredients
            .Select(i => new Ingredient(i.Name, i.Products))
            .ToList();

        var steps = command.Steps
            .Select(s => new Step(s.Instructions))
            .ToList();

        var tags = command.Tags
            .Select(t => new Tag(t.Name))
            .ToList();

        var recipe = new Recipe(
            command.Name,
            command.Description,
            command.TimeToCook,
            command.CountPersons,
            command.ImagePath,
            command.AuthorId,
            ingredients,
            steps,
            tags);

        await recipeRepository.AddAsync(recipe, cancellationToken);
    }
}