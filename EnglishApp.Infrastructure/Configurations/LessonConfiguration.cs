using EnglishApp.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishApp.Infrastructure.Configurations
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.Property(lesson => lesson.Name)
                .HasMaxLength(200);
            builder.Property(lesson => lesson.Video)
                .HasMaxLength(100);
            builder.Property(lesson => lesson.Audio)
                .HasMaxLength(100);
        }
    }
}
