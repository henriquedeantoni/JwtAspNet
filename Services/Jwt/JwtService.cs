using userJwtApp.Services.Jwt;
﻿using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens; 

public class JwtService
{
    private byte[] simetricKey { get; }

    public JwtService(byte[] SimetricKey)
    {
        this.simetricKey = simetricKey;
    }
    public string GenerateToken(Guid userId)
    {
        JsonWebTokenHandler handler = new();
        string jwt = handler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = new(new[]
            {
                new Claim(JwtConsts.CLAIM_ID, userId.ToString())
            }),
            SigningCredentials = new(new SymmetricSecurityKey(simetricKey),
                SecurityAlgorithms.HmacSha256Signature),
            Issuer = JwtConsts.JWT_ISSUER,
            IssuedAt = DateTime.Now
        });

        return jwt;
    }
}