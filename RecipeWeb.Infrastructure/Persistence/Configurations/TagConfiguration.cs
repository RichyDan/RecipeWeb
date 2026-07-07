using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeWeb.Domain.RecipeAggregate;

namespace RecipeWeb.Infrastructure.Persistence.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
               .IsRequired()
               .HasMaxLength(100);
    }
}
