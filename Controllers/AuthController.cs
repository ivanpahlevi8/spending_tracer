using Microsoft.AspNetCore.Mvc;
using SpendTracerApi.Models;
using SpendTracerApi.Models.Dtos;
using SpendTracerApi.Services.IServices;

namespace SpendTracerApi.Controllers
{
    public class AuthController : Controller
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            ResponseDto response = await _authService.RegisterUser(registerUserDto);

            // check response
            if(response.isSuccess)
            {
                return Ok(response);
            } else
            {
                return BadRequest(response);
            }
        }

        [HttpPost]
        [Route("login-user")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserRequestDto loginUserDto)
        {
            ResponseDto response = await _authService.LoginUser(loginUserDto);

            // check response
            if (response.isSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
