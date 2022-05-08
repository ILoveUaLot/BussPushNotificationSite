namespace BussPushNotification.Data.Interface
{
    public interface IRepository<T> : IDisposable
    {
        IQueryable<T> GetList();
        T GetItem(int id);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
        void Save();
    }
}
