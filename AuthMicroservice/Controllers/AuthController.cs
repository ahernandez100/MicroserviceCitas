using AuthMicroservice.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace AuthMicroservice.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
       // private const string SecretKey = "YourSuperSecureKeyThatHasAtLeast32Characters!";
        private readonly AuthenticationService _authService;

        public AuthController()
        {
            _authService = new AuthenticationService();
        }

        [HttpPost]
        [Route("token")]
        public IHttpActionResult GenerateToken([FromBody] LoginRequest login)
        {
            if (_authService.ValidateUser(login.Username, login.Password))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("YourSuperSecretKeyThatHasAtLeast32Characters!");

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, login.Username),
                        new Claim(ClaimTypes.Role, "User")
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { Token = tokenString });
            }

            return Unauthorized();
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
