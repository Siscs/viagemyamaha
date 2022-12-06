using Microsoft.AspNetCore.Mvc;
using ViagemYamaha.Core.Contracts.Api;

namespace ViagemYamaha.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult CustomResponse(object response)
        {
            if (response == null)
                return BadRequest(new { Data = "Rota não encontrada"} );

            return Ok(new SuccessResult { Data = response });
        }

        protected IActionResult CustomResponse(string response)
        {
            if (string.IsNullOrEmpty(response))
                return NotFound(new { Data = response });

            return Ok(new SuccessResult { Data = response });
        }
    }
}
