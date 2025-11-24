using EnglishApp.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnglishApp.Infrastructure.Configurations
{
    public class ExcerciseQuestionOptionConfiguration : IEntityTypeConfiguration<ExerciseQuestionOption>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ExerciseQuestionOption> builder)
        {
            builder.Property(questionType => questionType.Name)
                .HasMaxLength(200);
            builder.Property(questionType => questionType.Audio)
                .HasMaxLength(100);
        }
    }
}
