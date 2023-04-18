using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace YourWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login(UserModel user)
        {
            // Check if the user exists and the password is correct
            if (user.Username == "valid_username" && user.Password == "valid_password")
            {
                // Create claims for the user
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                };
                Console.WriteLine("sdifs");
                // Generate the token
                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"])),
                        SecurityAlgorithms.HmacSha256)
                );

                // Return the token as a string
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            // If the user does not exist or the password is incorrect, return a 401 Unauthorized response
            return Unauthorized();
        }
    }

    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}