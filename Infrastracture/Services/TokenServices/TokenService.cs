using Application.Interfaces.Services.TokenServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastracture.Services.TokenServices;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> GenerateToken(int userId, string Role)
    {
        var key = _configuration["JWTSettings:SecretKey"];
        var issuer = _configuration["JWTSettings:Issuer"];
        var audience = _configuration["JWTSettings:Audience"];
        var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        var claims = new Claim[]
        {
            new Claim("role", Role),
            new Claim("UserId", userId.ToString())
        };
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}


