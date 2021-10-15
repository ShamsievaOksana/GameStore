using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.DataModel
{
    public class ProductEntityConfiguration
        : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("Product")
                .HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(x => x.Published)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(x => x.Created)
                .HasColumnType("datetime2");
            
            builder.Property(x => x.Modified)
                .HasColumnType("datetime2");
        }
    }
}