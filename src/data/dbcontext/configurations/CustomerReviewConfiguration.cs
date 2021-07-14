using amazon_customer_reviews_data.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amazon_customer_reviews_data.dbcontext.configurations
{
    public class CustomerReviewConfiguration : IEntityTypeConfiguration<CustomerReview>
    {
        public void Configure(EntityTypeBuilder<CustomerReview> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => new { e.ProductId, e.UserId }).IsUnique();

            builder.Property(e => e.Created)
                .HasColumnType("timestamp");
            builder.Property(e => e.Updated)
                .HasColumnType("timestamp");
        }
    }
}
