using Microsoft.AspNetCore.Mvc;
using Businness.Abstract;
using Core.Dto;
using Core.Utilities.Security.JWT;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            try
            {
                _authService.Register(registerDto);
                return Ok(new { message = "Kayıt işlemi başarılı." });
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { error = errorMessage });
            }
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                Token token = _authService.Login(loginDto);
                if (token == null)
                    return Unauthorized(new { message = "Geçersiz kullanıcı adı veya şifre." });

                return Ok(token);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { error = errorMessage });
            }
        }

    }
}
