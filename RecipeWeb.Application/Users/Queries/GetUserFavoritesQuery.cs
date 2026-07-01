using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Users.Queries;

public record GetUserFavoritesQuery(Guid UserId) : IQuery<List<Guid>>;