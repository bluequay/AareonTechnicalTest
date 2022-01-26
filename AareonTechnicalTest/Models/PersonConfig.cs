using Microsoft.EntityFrameworkCore;

namespace AareonTechnicalTest.Models
{
    public static class PersonConfig
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(
                entity =>
                {
                    entity.HasKey(e => e.Id);
                });

            // Note this is just for testing purposes
            modelBuilder.Entity<Person>().HasData(
             new Person { Id = 999, Forename = "Test", Surname = "User", IsAdmin = false },
             new Person { Id = 1000, Forename = "Test", Surname = "Admin", IsAdmin = true });
        }
    }
}
