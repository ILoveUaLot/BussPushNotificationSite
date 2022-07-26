namespace BussPushNotification.Data.Interface
{
    public interface IRepository<T, TKey> : IDisposable
    {
        IQueryable<T> GetList();
        Task<T> GetItemAsync(TKey id);
        Task CreateAsync(T item);
        void Update(T item);
        Task DeleteAsync(T item);
        Task SaveAsync();
    }
}
