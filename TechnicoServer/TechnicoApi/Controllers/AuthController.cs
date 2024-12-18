using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TechnicoDomain.Context;
using TechnicoDomain.Models;
using TechnicoDomain.Dtos;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly TechnicoDbContext _context;

    public AuthController(IConfiguration configuration, TechnicoDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    // POST: api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var owner = await ValidateUserAsync(loginRequest);
        if (owner != null)
        {
            var token = GenerateJwtToken(owner);
            return Ok(new { token });
        }
        return Unauthorized("Invalid credentials");
    }

    //   GET: api/auth/me
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetLoggedOwner()
    {
        var ownerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "OwnerId")?.Value;

        if (string.IsNullOrEmpty(ownerIdClaim) || !long.TryParse(ownerIdClaim, out var ownerId))
        {
            return Unauthorized("Invalid token");
        }

        var owner = await _context.Owners.FirstOrDefaultAsync(o => o.Id == ownerId);

        if (owner == null)
        {
            return NotFound("Owner not found");
        }

        var ownerResponse = new OwnerResponseDto
        {
            Id = owner.Id,
            Name = owner.Name,
            Surname = owner.Surname,
            Email = owner.Email,
            VATNumber = owner.VATNumber,
            PhoneNumber = owner.PhoneNumber,
            Role = owner.OwnerType.ToString()
        };

        return Ok(ownerResponse);
    }

    private async Task<Owner?> ValidateUserAsync(LoginRequest loginRequest)
    {
        return await _context.Owners
            .FirstOrDefaultAsync(o => o.Email == loginRequest.Username && o.Password == loginRequest.Password);
    }

    private string GenerateJwtToken(Owner owner)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, owner.Name),
            new Claim(ClaimTypes.Email, owner.Email),
            new Claim("OwnerId", owner.Id.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
