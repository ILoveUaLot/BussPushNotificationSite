using BussPushNotification.Models;
using Microsoft.AspNetCore.Identity;

namespace BussPushNotification.Data.Interface
{
    public interface IUserRepository : IRepository<IdentityUser, Guid>
    {
    }
}
