using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static System.Text.Encoding;

namespace ECommerce;

[ApiController]
[Route("apis/[controller]")]
public abstract class AuthenticationController<TEntity> : ControllerBase where TEntity : Person
{
    protected readonly IConfiguration _configuration;
    protected readonly BaseUnitOfWork<TEntity> _baseUnitOfWork;
    protected readonly IMapper _mapper;
    public AuthenticationController(IConfiguration configuration, IMapper mapper, BaseUnitOfWork<TEntity> unitOfWork)
    {
        _configuration = configuration;
        _mapper = mapper;
        _baseUnitOfWork = unitOfWork;
    }
    [HttpPost]
    public async Task<IActionResult> Post(PersonCredentials credentials)
    {
        TEntity person = (await _baseUnitOfWork.ReadAsync(p => (p.Username == credentials.Username
                                                                && p.PasswordHash == credentials.Password.ToSHA256()))).FirstOrDefault();
        if (person == null) return NotFound($"{typeof(TEntity).ToString().Split('.')[^1]} not found");
        var claims = new[]{
            new Claim(ClaimTypes.Name,credentials.Username),
            new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Role,typeof(TEntity).ToString().Split('.')[^1])
        };
        var key = new SymmetricSecurityKey(UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(_configuration.GetSection("JWT:Issuer").Value,
                                         claims: claims,
                                         expires: DateTime.UtcNow.AddMinutes(20),
                                         signingCredentials: signIn);
        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
}
