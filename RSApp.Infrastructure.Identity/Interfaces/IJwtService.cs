using RSApp.Core.Services.Dtos.Account;
using RSApp.Infrastructure.Identity.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace RSApp.Infrastructure.Identity.interfaces;

public interface IJwtService {
  Task<JwtSecurityToken> GenerateJwToken(ApplicationUser user);
  RefreshToken GenerateRefreshToken();
}
