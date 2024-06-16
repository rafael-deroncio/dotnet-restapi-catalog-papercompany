using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PapperCompany.Catalog.Core.Services.Interfaces;
using PapperCompany.Catalog.Domain.Requests;
using PapperCompany.Catalog.Domain.Responses;

namespace PapperCompany.Catalog.Core.Services;

public class TokenService(
    IConfiguration configuration,
    ILogger<TokenService> logger
) : ITokenService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly ILogger<TokenService> _logger = logger;


    public async Task<TokenResponse> Generate(TokenRequest request)
    {
        _logger.LogInformation("Starting access token generation for the user '{0}' with role '{1}'", request.Username, request.Role);

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenExpires = GenerateTokenExpires();
            var tokenClaims = GenerateClaims(request.Role, request.Claims, request.Username);
            var tokenCredentials = GenerateTokenCredentials();
            var tokenDescriptor = GenerateTokenDescriptor(tokenExpires, tokenCredentials, tokenClaims);
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return await Task.FromResult(
                            new TokenResponse()
                            {
                                Message = "access-token-jwt-ok",
                                Token = "Bearer " + tokenHandler.WriteToken(token),
                                Expires = tokenExpires
                            }
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on proccess access token generation");
            throw;
        }
        finally
        {
            _logger.LogInformation("Finishing access token generation for the user '{0}' with role '{1}'", request.Username, request.Role);

        }
    }

    private DateTime GenerateTokenExpires()
    {
        var expireHours = double.Parse(_configuration["JwtSettings:ExpireHours"]);
        return DateTime.UtcNow.AddHours(expireHours);
    }

    private static ClaimsIdentity GenerateClaims(string role, IEnumerable<string> claims, string username)
    {
        ClaimsIdentity claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaim(new Claim(type: ClaimTypes.Role, value: role));
        claimsIdentity.AddClaim(new Claim(type: ClaimTypes.Name, value: username));


        claimsIdentity.AddClaims(
            claims.Select(claim =>
            {
                return new Claim(type: claim.ToString(), value: claim);
            }));


        return claimsIdentity;
    }

    private SigningCredentials GenerateTokenCredentials()
    {
        string key = _configuration["JwtSymmetricSecurityKey"];
        SymmetricSecurityKey symmetricKey = new(Encoding.UTF8.GetBytes(key));
        return new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
    }

    private static SecurityTokenDescriptor GenerateTokenDescriptor(DateTime tokenExpires, SigningCredentials tokenCredentials, ClaimsIdentity tokenClaims)
    {
        return new SecurityTokenDescriptor()
        {
            Subject = tokenClaims,
            Expires = tokenExpires,
            SigningCredentials = tokenCredentials
        };
    }
}