using Microsoft.EntityFrameworkCore;
using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Application.Users.Queries;

namespace RecipeWeb.Infrastructure.Persistence.QueryHandlers.Users;

public class GetUserLikesQueryHandler(RecipeDbContext context) : IQueryHandler<GetUserLikesQuery, List<Guid>>
{
    public async Task<List<Guid>> Handle(GetUserLikesQuery query, CancellationToken cancellationToken) =>
        await context.UserLikes
            .AsNoTracking()
            .Where(likes => likes.UserId == query.UserId)
            .Select(likes => likes.RecipeId)
            .ToListAsync(cancellationToken);
}