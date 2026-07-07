using Microsoft.EntityFrameworkCore;
using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Application.Users.Queries;

namespace RecipeWeb.Infrastructure.Persistence.QueryHandlers.Users;

public class GetUserFavoritesQueryHandler(RecipeDbContext context): IQueryHandler<GetUserFavoritesQuery, List<Guid>>
{
    public async Task<List<Guid>> Handle(GetUserFavoritesQuery query, CancellationToken cancellationToken) =>
        await context.UserFavorites
            .AsNoTracking()
            .Where(favorites => favorites.UserId == query.userId)
            .Select(favorites => favorites.RecipeId)
            .ToListAsync(cancellationToken);
}