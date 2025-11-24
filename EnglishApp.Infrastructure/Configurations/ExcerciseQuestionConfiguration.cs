using EnglishApp.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishApp.Infrastructure.Configurations
{
    public class ExcerciseQuestionConfiguration : IEntityTypeConfiguration<ExcerciseQuestion>
    {
        public void Configure(EntityTypeBuilder<ExcerciseQuestion> builder)
        {
            builder.Property(questionType => questionType.Question)
                .HasMaxLength(500);
        }
    }
}
