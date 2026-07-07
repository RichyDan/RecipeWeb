using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Users.Queries;

public record GetUserFavoritesQuery(Guid userId): IQuery<List<Guid>>;