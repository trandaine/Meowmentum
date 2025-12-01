using EnglishApp.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishApp.Infrastructure.Configurations
{
    public class CourseCustomerDetailConfiguration : IEntityTypeConfiguration<CourseCustomerDetail>
    {
        public void Configure(EntityTypeBuilder<CourseCustomerDetail> modelBuilder)
        {
            modelBuilder
                .HasKey(e => new { e.CustomerId, e.CourseId }); // Superkey
            modelBuilder.Property(e => e.Amount)
                .HasPrecision(18,2);
            modelBuilder.Property(x => x.TransactionId)
               .HasMaxLength(450);
            modelBuilder
                .HasOne(e => e.Customer)
                .WithMany(s => s.CourseCustomerDetails)
                .HasForeignKey(e => e.CustomerId);

            modelBuilder
                .HasOne(e => e.Course)
                .WithMany(c => c.CourseCustomerDetails)
                .HasForeignKey(e => e.CourseId);

        }
    }
}
