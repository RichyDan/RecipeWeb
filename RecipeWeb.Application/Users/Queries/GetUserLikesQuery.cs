using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Users.Queries;

public class GetUserLikesQuery : IQuery<List<Guid>>
{
    public Guid UserId { get; set; }
}