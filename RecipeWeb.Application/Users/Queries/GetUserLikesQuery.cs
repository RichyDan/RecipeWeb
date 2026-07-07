using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Application.Users.Queries;

public record GetUserLikesQuery(Guid userId) : IQuery<List<Guid>>;
