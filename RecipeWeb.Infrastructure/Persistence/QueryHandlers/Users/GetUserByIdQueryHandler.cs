using Microsoft.EntityFrameworkCore;
using RecipeWeb.Application.Common.DTOs;
using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Application.Users.Queries;

namespace RecipeWeb.Infrastructure.Persistence.QueryHandlers.Users;

public class GetUserByIdQueryHandler(RecipeDbContext context) : IQueryHandler<GetUserByIdQuery, UserDto?>
{
    public async Task<UserDto?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken) =>
        await context.Users
            .AsNoTracking()
            .Where(user => user.Id == query.UserId)
            .Select(user => new UserDto(
                user.Id,
                user.FirstName,
                user.Login,
                user.Description,
                user.LikedRecipes.Select(l => l.RecipeId).ToList(),
                user.FavoriteRecipes.Select(f => f.RecipeId).ToList()
            ))
            .FirstOrDefaultAsync(cancellationToken);
}