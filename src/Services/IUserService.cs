using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppAuthentication.Models;

namespace WebAppAuthentication.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
    }
}
