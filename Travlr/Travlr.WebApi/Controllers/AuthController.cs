using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Travlr.WebApi.Dtos;
using Travlr.WebApi.Models;

namespace Travlr.WebApi.Controllers
{
    /// <summary>
    /// Auth controller provides the /login and /register endpoints
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor injects the .NET UserManager and Configuration instances
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="configuration"></param>
        public AuthController(UserManager<ApplicationUser> userManager,  IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Register a new User
        /// Validates unique email and saves the new user to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            // return status 400: Bad Request if user already exists
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return BadRequest(new AuthResponseDto(false, "", "User already exists."));

            // otherwise, create the new user with the required fields
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name
            };

            // save the user to the database
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest(new AuthResponseDto(false, "", errors));
            }
            // status 200 OK
            return Ok(new AuthResponseDto(true, "", "User registered successfully."));
        }

        /// <summary>
        /// Login for existing user
        /// Validates user exists and checks the password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            // email serves as the username field
            var user = await _userManager.FindByEmailAsync(model.Email);
            //validate credentials
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized(new AuthResponseDto(false, "", "Invalid credentials."));

            // generate JWT token for session
            var token = GenerateJwtToken(user);
            return Ok(new AuthResponseDto(true, token, "Login successful."));
        }

        /// <summary>
        /// Generates a JSON Web Token used authenticate POST/PUT/DELETE
        /// requests to the trips endpoints
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GenerateJwtToken(ApplicationUser user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]!);

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("Name", user.Name)
        };

            // configure token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                // token currently configured to expire after 1 hour
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"]!)),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            // returns the token to the client
            return tokenHandler.WriteToken(token);
        }
    }
}
