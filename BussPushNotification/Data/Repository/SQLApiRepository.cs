using BussPushNotification.Data.Interface;
using BussPushNotification.Models;
using Microsoft.EntityFrameworkCore;

namespace BussPushNotification.Data.Repository
{
    public class SQLApiRepository : IApiRepositroy
    {
        BussNotificationContext db;
        public SQLApiRepository(BussNotificationContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(Api item)
        {
            await db.Apis.AddAsync(item);
        }

        public async Task DeleteAsync(Api item)
        {
            db.Remove(item);
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
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
