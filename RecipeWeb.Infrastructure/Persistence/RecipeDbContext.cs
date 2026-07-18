using Microsoft.EntityFrameworkCore;
using RecipeWeb.Domain.Common;
using RecipeWeb.Domain.RecipeAggregate;
using RecipeWeb.Domain.UserAggregate;

namespace RecipeWeb.Infrastructure.Persistence;

public class RecipeDbContext( DbContextOptions<RecipeDbContext> options ) : DbContext( options )
{
    public DbSet<Recipe> Recipes { get; set; }

    public DbSet<Ingredient> Ingredients { get; set; }

    public DbSet<Step> Steps { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<UserLike> UserLikes { get; set; }

    public DbSet<UserFavorite> UserFavorites { get; set; }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.Ignore<ValidationError>();

        modelBuilder.ApplyConfigurationsFromAssembly( typeof( RecipeDbContext ).Assembly );
    }
}
