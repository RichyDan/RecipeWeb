using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Domain.RecipeAggregate;

namespace RecipeWeb.Application.Recipes.Commands;

public class UpdateRecipeCommandHandler(IRecipeRepository recipeRepository) : ICommandHandler<UpdateRecipeCommand>
{
    public async Task Handle(UpdateRecipeCommand command, CancellationToken cancellationToken)
    {
        Recipe recipe = await recipeRepository.GetByIdAsync(command.RecipeId) ??
            throw new InvalidOperationException($"Рецепт с Id {command.RecipeId} не найден");

        var ingredients = command.Ingredients?
            .Select(i => new Ingredient(i.Name, i.Products))
            .ToList();

        var steps = command.Steps?
            .Select(s => new Step(s.Instructions))
            .ToList();

        var tags = command.Tags?
            .Select(t => new Tag(t.Name))
            .ToList();

        recipe.Update(
            command.Name,
            command.Description,
            command.TimeToCook,
            command.CountPersons,
            command.ImagePath,
            ingredients,
            steps,
            tags);
    }
}