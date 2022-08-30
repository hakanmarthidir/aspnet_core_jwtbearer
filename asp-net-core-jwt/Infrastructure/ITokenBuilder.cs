using System.Security.Claims;

namespace asp_net_core_jwt.Infrastructure
{
    public interface ITokenBuilder
    {
        string BuildToken(Claim[] claims);
        string BuildRefreshToken();
        ClaimsPrincipal GetPrincipalFromToken(string token);
    }

}

