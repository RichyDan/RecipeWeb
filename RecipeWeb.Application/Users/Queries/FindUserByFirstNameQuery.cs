using RecipeWeb.Application.Common.DTOs;
using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Users.Queries;

public class FindUserByFirstNameQuery : IQuery<UserDto?>
{
    public string FirstName { get; set; } = null!;
}