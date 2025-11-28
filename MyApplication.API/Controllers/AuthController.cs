using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApplication.API.Models.DTOs;
using MyApplication.API.Repositories.Interfaces;
using System.Threading.Tasks;

namespace MyApplication.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
   private readonly UserManager<IdentityUser> _userManager;
   private readonly ITokenRepository _tokenRepository;

   public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
   {
      this._userManager = userManager;
      this._tokenRepository = tokenRepository;
   }

   [HttpPost("register")]
   public async Task<ActionResult> Register([FromBody] RegisterRequestDto dto)
   {
      var identityUser = new IdentityUser
      {
         UserName = dto.Username,
         Email = dto.Username,
      };

      var identityResult = await _userManager.CreateAsync(identityUser, dto.Password);

      if (identityResult.Succeeded)
      {
         if (dto.Roles != null)
         {
            var rolesResult = await _userManager.AddToRolesAsync(identityUser, dto.Roles);

            if (rolesResult.Succeeded)
            {
               return Ok("Register successful");
            }
         }
      }

      return BadRequest("Register failed");
   }

   [HttpPost("login")]
   public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
   {
      var user = await _userManager.FindByEmailAsync(dto.Username);
      if (user != null)
      {
         var result = await _userManager.CheckPasswordAsync(user, dto.Password);

         if (result)
         {
            var roles = await _userManager.GetRolesAsync(user);

            // create the token and return it
            var token = _tokenRepository.CreateJWTToken(user, roles.ToList());

            var loginResponse = new LoginResponseDto
            {
               JwtToken = token,
            };

            return Ok(loginResponse);
         }

      }

      return Unauthorized("Invalid credentials");
   }

}

