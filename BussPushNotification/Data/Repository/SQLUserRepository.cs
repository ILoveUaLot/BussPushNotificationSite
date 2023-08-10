using BussPushNotification.Data.Interface;
using BussPushNotification.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BussPushNotification.Data.Repository
{
    public class SQLUserRepository : IUserRepository
    {
        private BussNotificationContext db;

        private bool disposedValue;

        public SQLUserRepository(BussNotificationContext context)
        {
            db = context;
        }
        
        public async Task CreateAsync(IdentityUser item)
        {
            await db.AddAsync(item);
        }

        public async Task DeleteAsync(IdentityUser item)
        {
            db.Remove(item);
            await Task.CompletedTask;
        }

        public async Task<IdentityUser> GetItemAsync(Guid id)
        {
            return await db.Users.FindAsync(id);
        }

        public IQueryable<IdentityUser> GetList()
        {
            return db.Users;
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Update(IdentityUser item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
