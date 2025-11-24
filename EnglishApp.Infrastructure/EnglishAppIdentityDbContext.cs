using EnglishApp.ApplicationCore.IdentityEntities;
using EnglishApp.Infrastructure.Constants;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EnglishApp.Infrastructure
{
    //public class EnglishAppIdentityDbContext(DbContextOptions<EnglishAppIdentityDbContext> options) : IdentityDbContext(options)
    public class EnglishAppIdentityDbContext : IdentityDbContext<EnglishAppIdentityUser>
    {
        public EnglishAppIdentityDbContext(DbContextOptions<EnglishAppIdentityDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = ConnectionConstants.SQL_SERVER_IDENTITY_CONNECTION_STRING;
            optionsBuilder.UseSqlServer(connectionString);
        }

        // Remove the DbSet declarations as they're already handled by the base class
        //public DbSet<EnglishAppIdentityUser> Users { get; set; }
        //public DbSet<EnglishAppIdentityRole> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Apply custom configurations if any

        }
    }
}
