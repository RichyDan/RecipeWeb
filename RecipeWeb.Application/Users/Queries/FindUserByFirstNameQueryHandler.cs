using RecipeWeb.Application.Common.DTOs;
using RecipeWeb.Application.Common.Interfaces;
using RecipeWeb.Domain.UserAggregate;

namespace RecipeWeb.Application.Users.Queries;

public class FindUserByFirstNameQueryHandler(IUserRepository userRepository) : IQueryHandler<FindUserByFirstNameQuery, UserDto?>
{
    public async Task<UserDto?> Handle(FindUserByFirstNameQuery query, CancellationToken cancellationToken)
    {
        User? user = await userRepository.FindByFirstNameAsync(query.FirstName);

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