using System.ComponentModel.DataAnnotations;

namespace asp_net_core_jwt.Application.Contracts
{
    public class SignInRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
