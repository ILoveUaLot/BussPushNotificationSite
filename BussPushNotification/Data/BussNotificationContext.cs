using BussPushNotification.Models;
using Microsoft.EntityFrameworkCore;
namespace BussPushNotification.Data
{
    public class BussNotificationContext : DbContext
    {
        public BussNotificationContext(DbContextOptions<BussNotificationContext> options)
            : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; } = null!;
    }
}
