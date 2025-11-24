using EnglishApp.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishApp.Infrastructure.Configurations
{
    public class QuestionTypeConfiguration : IEntityTypeConfiguration<QuestionType>
    {
        public void Configure(EntityTypeBuilder<QuestionType> builder)
        {
            builder.Property(questionType => questionType.Name)
                .HasMaxLength(200);
            builder.Property(questionType => questionType.Code)
                .HasMaxLength(50);
            builder.Property(questionType => questionType.Description)
                .HasMaxLength(500);



        }
    }
}
