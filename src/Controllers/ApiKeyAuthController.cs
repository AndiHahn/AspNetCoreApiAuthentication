using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAppAuthentication2.Controllers
{
    [Authorize(AuthenticationSchemes = "ApiKeyAuthentication")]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiKeyAuthController : ControllerBase
    {
        [HttpGet]
        public string HelloWorld()
        {
            return "ApiKey Auth successful";
        }
    }
}
