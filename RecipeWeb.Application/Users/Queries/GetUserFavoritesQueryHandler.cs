using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Domain.UserAggregate;

namespace RecipeWeb.Application.Users.Queries;

public class GetUserFavoritesQueryHandler(IUserRepository userRepository) : IQueryHandler<GetUserFavoritesQuery, List<Guid>>
{
    public async Task<List<Guid>> Handle(GetUserFavoritesQuery query, CancellationToken cancellationToken)
    {
        User user = await userRepository.GetByIdAsync(query.UserId);

        return user?.FavoriteRecipes.Select(f => f.RecipeId).ToList() ?? [];
    }
}