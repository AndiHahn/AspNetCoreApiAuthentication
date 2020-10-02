using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAppAuthentication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JwtAuthTestController : ControllerBase
    {
        //[Authorize(Roles = Constants.JwtAuthSection.UserRoles.User)]
        [HttpGet("userrole")]
        public string HelloWorldUserRole()
        {
            return "Successfully called method with user role";
        }

        //[Authorize(Roles = Constants.JwtAuthSection.UserRoles.Admin)]
        [HttpGet("adminrole")]
        public string HelloWorldAdminRole()
        {
            return "Successfully called method with admin role";
        }
    }
}
