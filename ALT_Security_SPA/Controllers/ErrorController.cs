using Microsoft.AspNetCore.Mvc;

namespace ALT_Security_SPA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("error/{code}")]
        public IActionResult Error(int code)
        {
            return new ObjectResult(new { Code = code });
        }
    }
}
