using BussPushNotification.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace BussPushNotification.Data
{
    public class BussNotificationContext : IdentityDbContext<User>
    {
        public BussNotificationContext(DbContextOptions<BussNotificationContext> options)
            : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; } = null!;
    }
}
