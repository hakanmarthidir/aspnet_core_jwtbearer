using asp_net_core_jwt.Application.Contracts;

namespace asp_net_core_jwt.Application;

public interface IAuthenticationService
{
    Task<SignInResponse> SignIn(string userName, string password);
    Task<SignInResponse> RefreshToken(string accessToken, string refreshToken);
}
