using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeWeb.Domain.UserAggregate;

namespace RecipeWeb.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(u => u.Login)
               .IsRequired()
               .HasMaxLength(50);

        builder.HasIndex(u => u.Login).IsUnique();

        builder.Property(u => u.Password)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(u => u.Description)
               .HasMaxLength(500);

        // Связь с лайками и избранным как отдельные таблицы
        builder.HasMany(u => u.LikedRecipes)
               .WithOne()
               .HasForeignKey(l => l.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.FavoriteRecipes)
               .WithOne()
               .HasForeignKey(f => f.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
