using asp_net_core_jwt.Application;
using asp_net_core_jwt.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace asp_net_core_jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var result = await _authenticationService.SignIn(request.UserName, request.Password).ConfigureAwait(false);

            return Ok(result);
        }

        [HttpPost("refreshtoken")]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var result = await _authenticationService.RefreshToken(request.AccessToken, request.RefreshToken);

            return Ok(result);
        }

        [Authorize(Policy = "MembersOnly")]
        [HttpGet]
        public IActionResult GetClaims()
        {
            //var authenticatedUserName = User?.Identity?.Name;
            return Ok(HttpContext.User.Claims.Select(x => x.Value).ToList());
        }
    }
}
