using asp_net_core_jwt.Domain;
using asp_net_core_jwt.Domain.TokenEntity;
using asp_net_core_jwt.Domain.UserEntity;
using Microsoft.EntityFrameworkCore;

namespace asp_net_core_jwt.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}

