using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using MyApplication.API.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyApplication.API.Repositories;

public class TokenRepository : ITokenRepository
{
   private readonly IConfiguration _configuration;

   public TokenRepository(IConfiguration configuration)
   {
      this._configuration = configuration;
   }

   public string CreateJWTToken(IdentityUser identityUser, List<string> roles)
   {
      // create claims 
      var claims = new List<Claim>()
      {
         new Claim(ClaimTypes.Email, identityUser.Email)
      };

      foreach (var role in roles)
      {
         claims.Add(new Claim(ClaimTypes.Role, role));
      }

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

      var credientials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(_configuration["Issuer"],
                                       _configuration["Audience"],
                                       claims,
                                       expires: DateTime.UtcNow.AddMinutes(15),
                                       signingCredentials: credientials);


      var stringToken = new JwtSecurityTokenHandler().WriteToken(token);

      return stringToken;
   }


}
