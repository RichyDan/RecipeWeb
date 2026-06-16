using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Domain.UserAggregate;

namespace RecipeWeb.Application.Users.Queries;

public class GetUserLikesQueryHandler(IUserRepository userRepository) : IQueryHandler<GetUserLikesQuery, List<Guid>>
{
    public async Task<List<Guid>> Handle(GetUserLikesQuery query, CancellationToken cancellationToken)
    {
        User user = await userRepository.GetByIdAsync(query.UserId);

        return user?.LikedRecipes.Select(l => l.RecipeId).ToList() ?? [];
    }
}