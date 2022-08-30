using asp_net_core_jwt.Domain;

namespace asp_net_core_jwt.Domain.UserEntity
{
    public interface IUserRepository
    {
        Task<User> SignInAsync(string userName, string password);
        Task<User> FindUserAsync(int id);
    }
}

