using Microsoft.EntityFrameworkCore;

namespace AareonTechnicalTest.Models
{
    public static class TicketConfig
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>(
                entity =>
                {
                    entity.HasKey(e => e.Id);
                });
        }
    }
}