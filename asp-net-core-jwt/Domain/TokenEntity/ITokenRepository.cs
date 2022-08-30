using asp_net_core_jwt.Domain;

namespace asp_net_core_jwt.Domain.TokenEntity
{
    public interface ITokenRepository
    {
        Task InsertAsync(RefreshToken refreshToken);
        Task<RefreshToken> GetRefreshToken(int userId, string refreshToken);
        void Remove(RefreshToken refreshToken);
    }
}

