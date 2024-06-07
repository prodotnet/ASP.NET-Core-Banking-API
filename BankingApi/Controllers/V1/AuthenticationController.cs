using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankingApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            // For testing, accept any user with a hardcoded password
            if (loginRequest.Username == "username" && loginRequest.Password == "1234")
            {
                var token =loginRequest.Username;
                return Ok(new { token });
            }

            return Unauthorized();
        }

        
    }

    // Models/LoginRequest.cs
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

}
