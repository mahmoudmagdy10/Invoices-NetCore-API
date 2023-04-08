using Invoices.API.BL.Interface;
using Invoices.API.BL.Models;
using Invoices.API.BL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRep _authRep;

        public AuthController(IAuthRep authRep)
        {
            _authRep = authRep;
        }

        [HttpPost("GetToken")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authRep.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
