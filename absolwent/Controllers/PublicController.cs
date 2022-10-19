using absolwent.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace absolwent.Controllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "public")]
    [ApiController]
    public class PublicController : ControllerBase
    {
        /// <summary>
        /// Sprawdzenie statusu systemu
        /// </summary>
        /// <returns></returns>
        [HttpGet("status")]
        [SwaggerResponse(200, "Response", typeof(Response))]
        public IActionResult Status()
        {
            return new Response();
        }
    }
}
