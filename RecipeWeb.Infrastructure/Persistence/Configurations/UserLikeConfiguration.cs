using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeWeb.Domain.RecipeAggregate;
using RecipeWeb.Domain.UserAggregate;

namespace RecipeWeb.Infrastructure.Persistence.Configurations;

public class UserLikeConfiguration : IEntityTypeConfiguration<UserLike>
{
    public void Configure(EntityTypeBuilder<UserLike> builder)
    {
        // Составной ключ из UserId и RecipeId
        builder.HasKey(ul => new { ul.UserId, ul.RecipeId });

        // Связь с пользователем
        builder.HasOne<User>()
               .WithMany(u => u.LikedRecipes)
               .HasForeignKey(ul => ul.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        // Связь с рецептом
        builder.HasOne<Recipe>()
               .WithMany()
               .HasForeignKey(ul => ul.RecipeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
