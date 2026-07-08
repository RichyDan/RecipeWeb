using RecipeWeb.Application.Common.DTOs;
using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Users.Queries;

public record FindUserByFirstNameQuery( string firstName ) : IQuery<UserDto?>;