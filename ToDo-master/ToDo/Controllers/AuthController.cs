using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Core;
using ToDo.Extensions;
using ToDo.Requests;

namespace ToDo.Controllers
{
    [AllowAnonymous]
    public class AuthController : ApiControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.Register(request.UserName, request.Email, request.Password);
            return result.Succeeded
                ? (IActionResult) Ok(result.ToApiResponse())
                : BadRequest(result.ToApiError());
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.Login(request.Email, request.Password);
            return result.Succeeded
                ? (IActionResult) Ok(result.ToApiResponse())
                : BadRequest(result.ToApiError());
        }
    }
}