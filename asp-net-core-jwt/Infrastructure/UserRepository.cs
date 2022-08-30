using System;
using asp_net_core_jwt.Domain;
using asp_net_core_jwt.Domain.UserEntity;
using Microsoft.EntityFrameworkCore;

namespace asp_net_core_jwt.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> SignInAsync(string userName, string password)
        {
            var user = await _context.Users.Where(f => f.Email == userName && f.Password == password).FirstOrDefaultAsync().ConfigureAwait(false);
            return user;
        }

        public async Task<User?> FindUserAsync(int id)
        {
            return await _context.Users.FindAsync(id).ConfigureAwait(false);
        }
    }
}

