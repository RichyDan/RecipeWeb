using Microsoft.EntityFrameworkCore;
using RecipeWeb.Application.Common.DTOs;
using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Application.Users.Queries;

namespace RecipeWeb.Infrastructure.Persistence.QueryHandlers.Users;

public class FindUserByFirstNameQueryHandler(RecipeDbContext context) : IQueryHandler<FindUserByFirstNameQuery, UserDto?>
{
    /// <inheritdoc/>
    public async Task<UserDto?> Handle(FindUserByFirstNameQuery query, CancellationToken cancellationToken) =>
        await context.Users
            .AsNoTracking()
            .Where(user => user.FirstName == query.firstName)
            .Select(user => new UserDto(
                user.Id,
                user.FirstName,
                user.Login,
                user.Description,
                user.LikedRecipes.Select(l => l.RecipeId).ToList(),
                user.FavoriteRecipes.Select(f => f.RecipeId).ToList()))
            .FirstOrDefaultAsync(cancellationToken);
}
