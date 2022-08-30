using asp_net_core_jwt.Domain;
using asp_net_core_jwt.Domain.UserEntity;

namespace asp_net_core_jwt.Application.Contracts;

public class SignInResponse
{
    public User User { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
