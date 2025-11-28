using Microsoft.AspNetCore.Identity;

namespace MyApplication.API.Repositories.Interfaces;

public interface ITokenRepository
{

   string CreateJWTToken(IdentityUser identityUser, List<string> roles);

}
