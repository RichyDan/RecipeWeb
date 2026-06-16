using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Recipes.Commands;

public class DeleteRecipeCommand : ICommand
{
    public Guid RecipeId { get; set; }
}