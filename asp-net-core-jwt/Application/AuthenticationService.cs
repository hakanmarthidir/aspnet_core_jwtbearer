using asp_net_core_jwt.Application.Config;
using asp_net_core_jwt.Application.Contracts;
using asp_net_core_jwt.Controllers;
using asp_net_core_jwt.Domain;
using asp_net_core_jwt.Domain.TokenEntity;
using asp_net_core_jwt.Domain.UserEntity;
using asp_net_core_jwt.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace asp_net_core_jwt.Application;

public class AuthenticationService : IAuthenticationService
{
    private readonly JwtTokenConfig _jwtTokenConfig;
    private readonly ITokenRepository _tokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITokenBuilder _tokenBuilder;

    public AuthenticationService(JwtTokenConfig jwtTokenConfig, ITokenRepository tokenRepository, IUserRepository userRepository, ITokenBuilder tokenBuilder)
    {
        _jwtTokenConfig = jwtTokenConfig;
        _tokenRepository = tokenRepository;
        _userRepository = userRepository;
        _tokenBuilder = tokenBuilder;
    }

    public async Task<SignInResponse> SignIn(string userName, string password)
    {
        if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException("Signin data could not be null");

        var user = await this._userRepository.SignInAsync(userName, password).ConfigureAwait(false);

        if (user == null)
            throw new ArgumentNullException("User could not be found.");


        var claims = new Claim[]
        {
                new Claim("UId", user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Email),
                new Claim(ClaimTypes.Role,"Client")
        };

        var result = new SignInResponse()
        {
            User = user,
            AccessToken = _tokenBuilder.BuildToken(claims),
            RefreshToken = _tokenBuilder.BuildRefreshToken()

        };

        var token = new RefreshToken
        {
            UserId = user.Id,
            Token = result.RefreshToken,
            IssuedAt = DateTime.Now,
            ExpiresAt = DateTime.Now.AddMinutes(_jwtTokenConfig.RefreshTokenExpiration)
        };

        await this._tokenRepository.InsertAsync(token).ConfigureAwait(false);
        return result;
    }

    public async Task<SignInResponse> RefreshToken(string accessToken, string refreshToken)
    {
        ClaimsPrincipal claimsPrincipal = _tokenBuilder.GetPrincipalFromToken(accessToken);
        if (claimsPrincipal == null) throw new ArgumentNullException("Claims could not be found.");

        var userId = claimsPrincipal.Claims.First(c => c.Type == "UId").Value;
        if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException("Claim principal could not be found.");

        var user = await _userRepository.FindUserAsync(Convert.ToInt32(userId));
        if (user == null) throw new ArgumentException("User is null");

        var token = await _tokenRepository.GetRefreshToken(user.Id, refreshToken).ConfigureAwait(false);
        if (token == null) throw new ArgumentException("Token is null");

        var claims = new Claim[]
            {
                new Claim("UId", user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Email),
                new Claim(ClaimTypes.Role,"Client")
            };

        var result = new SignInResponse()
        {
            User = user,
            AccessToken = _tokenBuilder.BuildToken(claims),
            RefreshToken = _tokenBuilder.BuildRefreshToken()

        };

        _tokenRepository.Remove(token);
        await _tokenRepository.InsertAsync(new RefreshToken { UserId = user.Id, Token = result.RefreshToken, IssuedAt = DateTime.Now, ExpiresAt = DateTime.Now.AddMinutes(_jwtTokenConfig.RefreshTokenExpiration) }).ConfigureAwait(false);

        return result;
    }


}
