using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppAuthentication;

namespace WebAppAuthentication2.Controllers
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
