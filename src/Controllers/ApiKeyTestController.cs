using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppAuthentication;

namespace WebAppAuthentication.Controllers
{
    [Authorize(AuthenticationSchemes = Constants.ApiKeySection.AUTHENTICATION_SCHEME)]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiKeyTestController : ControllerBase
    {
        [HttpGet]
        public string HelloWorld()
        {
            return "ApiKey Auth successful";
        }
    }
}
