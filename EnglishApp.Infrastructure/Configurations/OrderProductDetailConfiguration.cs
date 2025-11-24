using EnglishApp.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishApp.Infrastructure.Configurations
{
    public class OrderProductDetailConfiguration : IEntityTypeConfiguration<OrderProductDetail>
    {
        public void Configure(EntityTypeBuilder<OrderProductDetail> builder)
        {
            builder.HasKey(opd => new { opd.OrderId, opd.CourseId });
            builder
                .HasOne(opd => opd.Order)
                .WithMany(opd => opd.OrderProductDetails)
                .HasForeignKey(opd => opd.OrderId);

            builder
                .HasOne(opd => opd.Cousre)
                .WithMany(opd => opd.OrderProductDetails)
                .HasForeignKey(opd => opd.CourseId);

            builder.Property(opd => opd.CoursePrice)
                .HasPrecision(18, 2);
            builder.Property(opd => opd.PriceDiscounted)
                .HasPrecision(18, 2);

        }
    }
}
