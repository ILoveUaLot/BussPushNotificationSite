using BussPushNotification.Data.Interface;
using BussPushNotification.Models;
using Microsoft.EntityFrameworkCore;

namespace BussPushNotification.Data.Repository
{
    public class SQLUserRepository : IRepository<User>
    {
        private BussNotificationContext db;

        private bool disposedValue;

        public SQLUserRepository(IApplicationBuilder app)
        {
            db = app.ApplicationServices.CreateScope().
                ServiceProvider.GetRequiredService<BussNotificationContext>();
        }

        public void Create(User item)
        {
            db.Add(item);
        }

        public void Delete(User item)
        {
            db.Remove(item);
        }

        public User GetItem(int id)
        {
            return db.Users.Find(id);
        }

        public IQueryable<User> GetList()
        {
            return db.Users;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(User item)
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
