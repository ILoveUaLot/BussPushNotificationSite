using BussPushNotification.Models;
namespace BussPushNotification.Data.Interface
{
    public interface IUserRepository : IRepository<User, Guid>
    {
    }
}
