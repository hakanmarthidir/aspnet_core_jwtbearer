namespace asp_net_core_jwt.Application.Contracts
{
    public class RefreshTokenRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

}
