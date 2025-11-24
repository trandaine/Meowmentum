using EnglishApp.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishApp.Infrastructure.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(course => course.Name)
                .HasMaxLength(200);
            builder.Property(course => course.Description)
                .HasMaxLength(2000);
            builder.Property(course => course.Thumbnail)
                .HasMaxLength(100);
            builder.Property(course => course.Price)
                .HasPrecision(18, 2);
            //builder.HasOne(course => course.Currency)
            //    .WithMany()
            //    .HasForeignKey(c => c.CurrencyId);
        }
    }
}
