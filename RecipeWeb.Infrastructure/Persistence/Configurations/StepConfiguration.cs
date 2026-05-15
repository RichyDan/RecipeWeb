using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeWeb.Domain.RecipeAggregate;

namespace RecipeWeb.Infrastructure.Persistence.Configurations;

public class StepConfiguration : IEntityTypeConfiguration<Step>
{
    public void Configure(EntityTypeBuilder<Step> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Instructions)
               .IsRequired()
               .HasMaxLength(5000);

        builder.HasOne<Recipe>()
               .WithMany(r => r.Steps)
               .HasForeignKey("RecipeId")
               .OnDelete(DeleteBehavior.Cascade);
    }
}
