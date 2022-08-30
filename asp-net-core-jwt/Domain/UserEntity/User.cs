using System;
using System.ComponentModel.DataAnnotations;

namespace asp_net_core_jwt.Domain.UserEntity
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

