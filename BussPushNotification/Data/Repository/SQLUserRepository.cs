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
        
        public void Create(IdentityUser item)
        {
            db.Add(item);
        }

        public void Delete(IdentityUser item)
        {
            db.Remove(item);
        }

        public IdentityUser GetItem(Guid id)
        {
            return db.Users.Find(id);
        }

        public IQueryable<IdentityUser> GetList()
        {
            return db.Users;
        }

        public void Save()
        {
            db.SaveChanges();
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
