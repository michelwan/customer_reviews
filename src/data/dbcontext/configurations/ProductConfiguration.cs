using amazon_customer_reviews_data.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amazon_customer_reviews_data.dbcontext.configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Asin).IsUnique();

            builder.Property(e => e.Created)
                .HasColumnType("timestamp");
            builder.Property(e => e.Updated)
                .HasColumnType("timestamp");
        }
    }
}
