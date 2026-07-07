using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeWeb.Domain.RecipeAggregate;
using RecipeWeb.Domain.UserAggregate;

namespace RecipeWeb.Infrastructure.Persistence.Configurations;

public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Name).IsRequired().HasMaxLength(200);
        builder.Property(r => r.Description).IsRequired().HasMaxLength(2000);
        builder.Property(r => r.TimeToCook).IsRequired();
        builder.Property(r => r.CountPersons).IsRequired();
        builder.Property(r => r.ImagePath).HasMaxLength(500);

        // Связь с ингредиентами
        builder.HasMany(r => r.Ingredients)
               .WithOne()
               .HasForeignKey("RecipeId")
               .OnDelete(DeleteBehavior.Cascade);

        // Аналогично шаги
        builder.HasMany(r => r.Steps)
               .WithOne()
               .HasForeignKey("RecipeId")
               .OnDelete(DeleteBehavior.Cascade);

        // Многие ко многим с тегами
        builder.HasMany(r => r.Tags)
               .WithMany()
               .UsingEntity(j => j.ToTable("RecipeTags"));

        builder.HasOne<User>()
               .WithMany()
               .HasForeignKey(r => r.AuthorId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
