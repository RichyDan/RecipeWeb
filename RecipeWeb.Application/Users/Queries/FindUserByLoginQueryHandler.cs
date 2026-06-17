using RecipeWeb.Application.Common.DTOs;
using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Domain.UserAggregate;

namespace RecipeWeb.Application.Users.Queries;

public class FindUserByLoginQueryHandler(IUserRepository userRepository) : IQueryHandler<FindUserByLoginQuery, UserDto?>
{
    public async Task<UserDto?> Handle(FindUserByLoginQuery query, CancellationToken cancellationToken)
    {
        User? user = await userRepository.FindByLoginAsync(query.Login);

        return user is null ? null : MapToDto(user);
    }

    private static UserDto MapToDto(User user) =>
        new(
            user.Id,
            user.FirstName,
            user.Login,
            user.Description,
            user.LikedRecipes.Select(l => l.RecipeId).ToList(),
            user.FavoriteRecipes.Select(f => f.RecipeId).ToList()
        );
}