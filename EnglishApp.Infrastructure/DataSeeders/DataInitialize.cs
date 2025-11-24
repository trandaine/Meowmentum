using EnglishApp.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishApp.Infrastructure.DataSeeders
{
    public static class DataInitialize
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<EnglishAppDbContext>();
            context.Database.EnsureCreated();

            await context.Database.MigrateAsync();

            if (!context.Customers.Any())
            {
                context.Customers.AddRange(
                new Customer { Name = "John Doe", Email = "abx", UserId = "user1", Avartar = "avatar1.png" }
                );

            }
        }
    }
}
