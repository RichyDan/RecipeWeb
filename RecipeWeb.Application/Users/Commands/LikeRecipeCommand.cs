using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Users.Commands;

public class LikeRecipeCommand : ICommand
{
    public Guid UserId { get; set; }
    public Guid RecipeId { get; set; }
}