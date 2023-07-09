using BussPushNotification.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace BussPushNotification.Data
{
    public class BussNotificationContext : IdentityDbContext<IdentityUser>
    {
        public BussNotificationContext(DbContextOptions<BussNotificationContext> options)
            : base(options)
        {
            
        }
        public DbSet<IdentityUser> Users { get; set; } = null!;
    }
}
