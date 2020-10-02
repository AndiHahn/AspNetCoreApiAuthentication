using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppAuthentication;

namespace WebAppAuthentication.Controllers
{
    [Authorize(AuthenticationSchemes = Constants.BasicAuthSection.AUTHENTICATION_SCHEME)]
    [Route("api/[controller]")]
    [ApiController]
    public class BasicAuthTestController : ControllerBase
    {
        [HttpGet]
        public string HelloWorld()
        {
            return "Basic Auth successful";
        }
    }
}
