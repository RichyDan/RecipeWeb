using RecipeWeb.Application.Common.DTOs;
using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Users.Queries;

public class GetUserByIdQuery : IQuery<UserDto?>
{
    public Guid UserId { get; set; }
}