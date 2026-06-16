using RecipeWeb.Application.Common.DTOs;
using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Users.Queries;

public class FindUserByLoginQuery : IQuery<UserDto?>
{
    public string Login { get; set; } = null!;
}