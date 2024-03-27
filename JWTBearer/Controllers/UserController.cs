using JWTBearer.Application.Service;
using JWTBearer.Application.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JWTBearer.Domain.Entities;
using Azure;
using Microsoft.AspNetCore.Hosting.Server;
using System.Data;
using System.Text;

namespace JWTBearer.Controllers
{
    [Route("User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(UserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userDto)
        {
            try
            {
                bool registrationResult = await _userService.AddAsync(userDto);

                if (registrationResult)
                {
                    return Ok("User registered successfully");
                }

                return BadRequest("User registration failed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            bool loginResult = await _userService.GetByUserNameAsync(userLoginDto);

            if (loginResult)
            {
                string token = CreateToken(userLoginDto);
                return Ok(token);
            }
            else
            {
                return BadRequest("Invalid username or password");
            }
        }

        private string CreateToken(UserLoginDto userLoginDto)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userLoginDto.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;

            //When a user logs in to the Angular application, their credentials are typically sent to the server(usually the.NET Web API) for authentication.

            ////public class HashingHelper : IHashingHelper
            //{
            //    public string HashPassword(string password)
            //    {
            //        using (SHA256 sha256 = SHA256.Create())
            //        {
            //            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            //            StringBuilder builder = new StringBuilder();
            //            for (int i = 0; i < bytes.Length; i++)
            //            {
            //                builder.Append(bytes[i].ToString("x2"));
            //            }

            //            return builder.ToString();
            //        }
            //    }
            //}
            //     Upon successful authentication, the server generates a JWT containing claims(e.g., user ID, roles, permissions) and signs it using a secret key.
            //    This JWT is then sent back to the Angular application as part of the response.





        }
    }
}
