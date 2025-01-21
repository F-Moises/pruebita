using Identity.Application.Contracts;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser(UsuarioPersonalizado usuario)
        {
            var result = await _authService.CreateUser(usuario);   
            if (result)
            {
                return Created();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
