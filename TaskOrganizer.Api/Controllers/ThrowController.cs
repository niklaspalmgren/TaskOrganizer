using Microsoft.AspNetCore.Mvc;

namespace WebStep.Api.Controllers
{
    [ApiController]
    public class ThrowController : ControllerBase
    {

        [Route("api/Throw")]
        [HttpGet]
        public IActionResult Throw()
        {
            throw new Exception("This is an example exeption.");
        }
    }
}
