namespace BussPushNotification.Data.Interface
{
    public interface IRepository<T, TKey> : IDisposable
    {
        IQueryable<T> GetList();
        T GetItem(TKey id);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
        void Save();
    }
}
