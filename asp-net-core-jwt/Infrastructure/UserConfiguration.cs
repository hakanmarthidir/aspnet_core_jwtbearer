using asp_net_core_jwt.Domain;
using asp_net_core_jwt.Domain.UserEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace asp_net_core_jwt.Infrastructure
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User { Id = 1, Email = "test@test.com", Password = "test" }
            );
        }
    }
}

