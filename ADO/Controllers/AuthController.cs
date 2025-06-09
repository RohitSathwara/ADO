using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using ADO.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ADO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly JwtHelper _jwtHelper;

        public AuthController(IConfiguration config)
        {
            _config = config;
            _jwtHelper = new JwtHelper(config);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User login)
        {
            // Validate username/password here, for example check from DB or hardcoded for testing

            if (login.Username == "admin" && login.Password == "password")
            {
                var token = _jwtHelper.GenerateToken(login.Username, "Admin");
                return Ok(new { token });
            }
            else if (login.Username == "user" && login.Password == "password")
            {
                var token = _jwtHelper.GenerateToken(login.Username, "User");
                return Ok(new { token });
            }

            return Unauthorized();
        }
    }
}
