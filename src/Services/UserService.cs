using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppAuthentication2.Models;

namespace WebAppAuthentication2.Services
{
    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                Username = "username",
                Password = "password"
            }
        };

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(
                    () => _users.SingleOrDefault(x => x.Username == username &&
                                                      x.Password == password));

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so return user details without password
            user.Password = null;
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await Task.Run(() => _users.Select(
                u =>
                {
                    u.Password = null;
                    return u;
                }));
        }
    }
}
