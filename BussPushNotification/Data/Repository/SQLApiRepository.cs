using BussPushNotification.Data.Interface;
using BussPushNotification.Models;
using Microsoft.EntityFrameworkCore;

namespace BussPushNotification.Data.Repository
{
    public class SQLApiRepository : IApiRepository
    {
        BussNotificationContext db;
        public SQLApiRepository(BussNotificationContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(Api item)
        {
            db.Apis.AddAsync(item);
        }

        public async Task DeleteAsync(Api item)
        {
            db.Remove(item);
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            GC.Collect(1);
            db.Dispose();
        }

        public async Task<Api> GetItemAsync(string id)
        {
            return await db.Apis.FindAsync(id);
        }

        public IQueryable<Api> GetList()
        {
            return db.Apis.AsQueryable();
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Update(Api item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
