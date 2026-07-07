using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeWeb.Domain.RecipeAggregate;
using RecipeWeb.Domain.UserAggregate;

namespace RecipeWeb.Infrastructure.Persistence.Configurations;

public class UserFavoriteConfiguration : IEntityTypeConfiguration<UserFavorite>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<UserFavorite> builder)
    {
        builder.HasKey(uf => new { uf.UserId, uf.RecipeId });

        builder.HasOne<User>()
               .WithMany(u => u.FavoriteRecipes)
               .HasForeignKey(uf => uf.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Recipe>()
               .WithMany()
               .HasForeignKey(uf => uf.RecipeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
