using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeWeb.Domain.RecipeAggregate;

namespace RecipeWeb.Infrastructure.Persistence.Configurations
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.OwnsMany(i => i.Products, productBuilder =>
            {
                productBuilder.WithOwner().HasForeignKey("IngredientId");
                productBuilder.Property<string>("Value").IsRequired().HasMaxLength(200);
                productBuilder.HasKey("IngredientId", "Value"); // составной ключ
            });

            builder.HasOne<Recipe>()
                   .WithMany(r => r.Ingredients)
                   .HasForeignKey("RecipeId")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
