using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppAuthentication2.Models;

namespace WebAppAuthentication2.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
    }
}
