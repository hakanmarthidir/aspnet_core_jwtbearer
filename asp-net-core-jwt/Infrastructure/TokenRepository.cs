using asp_net_core_jwt.Domain;
using asp_net_core_jwt.Domain.TokenEntity;
using Microsoft.EntityFrameworkCore;

namespace asp_net_core_jwt.Infrastructure
{
    public class TokenRepository : ITokenRepository
    {
        private readonly ApplicationDbContext _context;

        public TokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task InsertAsync(RefreshToken refreshToken)
        {
            //new RefreshToken { UserId = user.Id, Token = result.RefreshToken, IssuedAt = DateTime.Now, ExpiresAt = DateTime.Now.AddMinutes(_jwtTokenConfig.RefreshTokenExpiration) }
            await _context.RefreshTokens.AddAsync(refreshToken).ConfigureAwait(false);
            _context.SaveChanges();
        }

        public async Task<RefreshToken?> GetRefreshToken(int userId, string refreshToken)
        {
            return await _context.RefreshTokens.Where(f => f.UserId == userId && f.Token == refreshToken && f.ExpiresAt >= DateTime.Now).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public void Remove(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Remove(refreshToken);
            _context.SaveChanges();
        }
    }
}

