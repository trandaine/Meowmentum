using EnglishApp.ApplicationCore.Entities;
using EnglishApp.Infrastructure.Configurations;
using EnglishApp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;

namespace EnglishApp.Infrastructure
{
    public class EnglishAppDbContext(DbContextOptions<EnglishAppDbContext> options) : DbContext(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionString = "Server= " + Constants.SQL_SERVER + " Database=MusicDbContext; User Id=sa; password=Dai@2018; TrustServerCertificate=True; Trusted_Connection=False; MultipleActiveResultSets=true;";
            var connectionString = ConnectionConstants.SQL_SERVER_CONNECTION_STRING;
            optionsBuilder.UseSqlServer(connectionString
                //sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()
                );

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CourseCommentConfiguration());
            builder.ApplyConfiguration(new CourseConfiguration());
            builder.ApplyConfiguration(new CourseCustomerDetailConfiguration());
            builder.ApplyConfiguration(new CurrencyConfiguration());
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new CustomerPaymentRecordConfiguration());
            builder.ApplyConfiguration(new CustomerProgressConfiguration());
            builder.ApplyConfiguration(new ExcerciseConfiguration());
            builder.ApplyConfiguration(new ExcerciseQuestionConfiguration());
            builder.ApplyConfiguration(new ExcerciseQuestionOptionConfiguration());
            builder.ApplyConfiguration(new LessonConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new OrderProductDetailConfiguration());
            builder.ApplyConfiguration(new QuestionTypeConfiguration());
            builder.ApplyConfiguration(new VocabularyConfiguration());
            builder.ApplyConfiguration(new CartItemConfiguration());
            builder.ApplyConfiguration(new ShoppingCartConfiguration());

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseComment> CourseComments { get; set; }
        public DbSet<CourseCustomerDetail> CourseCustomerDetails { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CustomerPaymentRecord> CustomerPaymentRecords { get; set; }
        public DbSet<CustomerProgress> CustomerProgresses { get; set; }
        public DbSet<Excercise> Excercises { get; set; }
        public DbSet<ExcerciseQuestion> ExcerciseQuestions { get; set; }
        public DbSet<ExerciseQuestionOption> ExerciseQuestionOptions { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderProductDetail> OrderProductDetails { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<Vocabulary> Vocabularies { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}
