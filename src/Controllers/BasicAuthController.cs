using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAppAuthentication2.Controllers
{
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [Route("api/[controller]")]
    [ApiController]
    public class BasicAuthController : ControllerBase
    {
        public BasicAuthController()
        {

        }

        [HttpGet]
        public string HelloWorld()
        {
            return "Basic Auth successful";
        }
    }
}
