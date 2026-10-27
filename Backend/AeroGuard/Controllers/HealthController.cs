using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AeroGuard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                service = "AeroGuard Backend",
                status = "Running"
            });
        }
    }
}
