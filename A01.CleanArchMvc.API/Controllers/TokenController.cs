using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using A01.CleanArchMvc.API.DTOs;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace A01.CleanArchMvc.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IAuthenticate _authentication;
    private readonly IConfiguration _configuration;

    public TokenController(IAuthenticate authentication, IConfiguration configuration)
    {
        _authentication = authentication;
        _configuration = configuration;
    }
    
    [AllowAnonymous]
    [HttpPost("LoginUser")]
    public async Task<ActionResult<UserTokenDto>> LoginAsync([FromBody] LoginDto userInfo)
    {
        var result = await _authentication.Authenticate(userInfo.Email, userInfo.Password);

        if (result)
            return GenerateToken(userInfo);
        
        ModelState.AddModelError(string.Empty, "Login inválido");
        return BadRequest(ModelState);
    }
    
    [HttpPost("CreateUser")]
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<ActionResult> CreateUserAsync([FromBody] LoginDto userInfo)
    {
        var result = await _authentication.RegisterUser(userInfo.Email, userInfo.Password);

        if (result)
            return Ok($"Usuário {userInfo.Email} criado com sucesso!");
    
        ModelState.AddModelError(string.Empty, "Registro inválido");
        return BadRequest(ModelState);
    }

    private UserTokenDto GenerateToken(LoginDto userInfo)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddMinutes(10);
        
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

        return new UserTokenDto()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
        
    }
}