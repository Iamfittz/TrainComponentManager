using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Train_Management_App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase {
    private readonly IConfiguration _config;

    public AuthController(IConfiguration config) {
        this._config = config;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request) {
        if (request.Username == "admin" && request.Password == "admin") {
            var token = GenerateToken(request.Username);
            return Ok(new { token });
        }

        return Unauthorized("Invalid credentials");
    }

    private string GenerateToken(string username) {
        var jwtSettings = _config.GetSection("JwtSettings");
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
        var creds = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpiresInMinutes"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public class LoginRequest {
    [Required]
    [MinLength(5)]
    public string Username { get; set; } = string.Empty;
    [Required]
    [MinLength(5)]
    public string Password { get; set; } = string.Empty;
}
