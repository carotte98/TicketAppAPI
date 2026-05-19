using Microsoft.EntityFrameworkCore;
using TicketApp.Models.Entities;

namespace TicketApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) 
        {
        }

        public DbSet<App> Apps { get; set; }
    }
}
