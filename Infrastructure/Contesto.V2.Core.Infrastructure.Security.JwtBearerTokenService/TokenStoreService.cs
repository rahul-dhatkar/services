//-------------------------------------------------------------------------------------------
//** Copyright © 2018, Fulcrum Digital                                  **
//** All rights reserved.                                                                  **
//**                                                                                       **
//** Redistribution, re-engineering or use of this code - in source                        **
//** or binary forms with or without modifications, are not                                **
//** permitted without prior written consent from appropriate person                       **
//** in Fulcrum Digital                                                 **
//**                                                                                       **
//**                                                                                       **
//** Author    : Fulcrum World Wide                                                        **
//** Created   : 08-06-18                                                                  **
//** Purpose   : TokenStoreService                                                         **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      08-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService.Dtos;
using Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService.Interfaces;
using Contesto.V2.Core.Infrastructures.Utility.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
namespace Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService
{
    /// <summary>
    /// Token Store Service
    /// </summary>
    /// <seealso cref="ITokenStoreService" />
    public class TokenStoreService : ITokenStoreService
    {
        private const string _securityKeySuffix = "DUryuJejJml0X7QZgaC9PyO5Xbpu0YjOvNoxRNfzk5k=";
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IIdentityUnitOfWork _unitOfWork;

        private readonly IOptionsSnapshot<JwtBearerTokensOptions> _jwtBearerConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenStoreService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="jwtBearerConfiguration">The JWT bearer configuration.</param>
        public TokenStoreService(IIdentityUnitOfWork unitOfWork, IOptionsSnapshot<JwtBearerTokensOptions> jwtBearerConfiguration)
        {
            _unitOfWork = unitOfWork;
            _jwtBearerConfiguration = jwtBearerConfiguration;

        }

        /// <summary>
        /// Adds the user token asynchronous.
        /// </summary>
        /// <param name="userToken">The user token.</param>
        /// <returns></returns>
        public async Task AddUserTokenAsync(UserTokenDomainModel userToken)
        {
            if (!_jwtBearerConfiguration.Value.AllowMultipleLoginsFromTheSameUser)
            {
                await InvalidateUserTokensAsync(userToken.UserId);
            }
            if (!string.IsNullOrEmpty(userToken.RefreshTokenSource))
            await DeleteTokensWithSameRefreshTokenSourceAsync(userToken.RefreshTokenSource);
            var id = await _unitOfWork.CommandUserTokenRepository.CreateTokens(userToken);
            //Create(userToken);
        }

        /// <summary>
        /// Adds the user token asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="refreshTokenSource">The refresh token source.</param>
        /// <returns></returns>
        public Task AddUserTokenAsync(UserDomainModel user, string refreshToken, string accessToken, string refreshTokenSource)
        {
            var now = DateTimeOffset.UtcNow;
            var token = new UserTokenDomainModel
            {
                UserId = user.UserId,
                // Refresh token handles should be treated as secrets and should be stored hashed
                RefreshToken = SecurityHelper.Instance.EncryptMessage(refreshToken, _securityKeySuffix),
                RefreshTokenSource = string.IsNullOrWhiteSpace(refreshTokenSource) ?
                                           null : SecurityHelper.Instance.EncryptMessage(refreshTokenSource, _securityKeySuffix),
                AccessToken = SecurityHelper.Instance.EncryptMessage(accessToken, _securityKeySuffix),
                RefreshTokenExpiresDateTime = now.AddMinutes(_jwtBearerConfiguration.Value.RefreshTokenExpirationMinutes),
                AccessTokenExpiresDateTime = now.AddMinutes(_jwtBearerConfiguration.Value.AccessTokenExpirationMinutes)
            };
            return AddUserTokenAsync(token);
        }

        /// <summary>
        /// Creates the JWT tokens.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="refreshTokenSource">The refresh token source.</param>
        /// <returns></returns>
        public async Task<(string accessToken, string refreshToken, IEnumerable<Claim> Claims)> CreateJwtTokens(UserDomainModel user, string refreshTokenSource)
        {
            var result = CreateAccessToken(user);
            var refreshToken = Guid.NewGuid().ToString().Replace("-", "");
            await AddUserTokenAsync(user, refreshToken, result.AccessToken, refreshTokenSource);
            return (result.AccessToken, refreshToken, result.Claims);
        }

        /// <summary>
        /// Deletes the expired tokens asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task DeleteExpiredTokensAsync() => _unitOfWork.CommandUserTokenRepository.DeleteExpiredTokens(null, null, null);

        /// <summary>
        /// Deletes the token asynchronous.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns></returns>
        public async Task DeleteTokenAsync(string refreshToken) => await _unitOfWork.CommandUserTokenRepository.DeleteExpiredTokens(null, SecurityHelper.Instance.EncryptMessage(refreshToken, _securityKeySuffix), null);

        /// <summary>
        /// Deletes the tokens with same refresh token source asynchronous.
        /// </summary>
        /// <param name="refreshTokenSource">The refresh token source.</param>
        /// <returns></returns>
        public Task DeleteTokensWithSameRefreshTokenSourceAsync(string refreshTokenSource) => _unitOfWork.CommandUserTokenRepository.DeleteExpiredTokens(null, null, SecurityHelper.Instance.EncryptMessage(refreshTokenSource, _securityKeySuffix));

        /// <summary>
        /// Finds the token asynchronous.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<UserTokenDomainModel> FindTokenAsync(string refreshToken)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Invalidates the user tokens asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public Task InvalidateUserTokensAsync(string userId)
        {
            return _unitOfWork.CommandUserTokenRepository.DeleteExpiredTokens(userId, null, null);
        }

        /// <summary>
        /// Determines whether [is valid token asynchronous] [the specified access token].
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<bool> IsValidTokenAsync(string accessToken, string userId)
        {
            bool result = false;
            var accessTokenHash = SecurityHelper.Instance.EncryptMessage(accessToken, _securityKeySuffix);
            var userToken = await _unitOfWork.QueryUserTokenRepository.GetByUserId(userId);

            if (userToken != null)
            {
                if (userToken.AccessToken == accessTokenHash)
                    result = userToken.AccessTokenExpiresDateTime >= DateTimeOffset.UtcNow;
            }

            return result;
        }

        /// <summary>
        /// Renews the token.
        /// </summary>
        /// <param name="userToken">The user token.</param>
        /// <returns></returns>
        public async Task RenewToken(UserTokenDomainModel userToken)
        {
            var now = DateTimeOffset.UtcNow;
            userToken.RefreshTokenExpiresDateTime = now.AddMinutes(_jwtBearerConfiguration.Value.RefreshTokenExpirationMinutes);
            userToken.AccessTokenExpiresDateTime = now.AddMinutes(_jwtBearerConfiguration.Value.AccessTokenExpirationMinutes);
            await _unitOfWork.CommandUserTokenRepository.UpdateTokens(userToken);
        }

        /// <summary>
        /// Revokes the user bearer tokens asynchronous.
        /// </summary>
        /// <param name="userIdValue">The user identifier value.</param>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns></returns>
        public async Task RevokeUserBearerTokensAsync(string userIdValue, string refreshToken)
        {
            if (!string.IsNullOrWhiteSpace(userIdValue))
            {
                if (_jwtBearerConfiguration.Value.AllowSignoutAllUserActiveClients)
                {
                    await InvalidateUserTokensAsync(userIdValue);
                }
            }

            if (!string.IsNullOrWhiteSpace(refreshToken))
            {
                await DeleteTokensWithSameRefreshTokenSourceAsync(refreshToken);
            }

            await DeleteExpiredTokensAsync();
        }

        /// <summary>
        /// Creates the access token asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        private (string AccessToken, IEnumerable<Claim> Claims) CreateAccessToken(UserDomainModel user)
        {
            var claims = new List<Claim>
            {
                // Unique Id for all Jwt tokes
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, _jwtBearerConfiguration.Value.Issuer),
                // Issuer
                new Claim(JwtRegisteredClaimNames.Iss, _jwtBearerConfiguration.Value.Issuer, ClaimValueTypes.String, _jwtBearerConfiguration.Value.Issuer),
                // Issued at
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _jwtBearerConfiguration.Value.Issuer),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString(), ClaimValueTypes.String, _jwtBearerConfiguration.Value.Issuer),
                new Claim(ClaimTypes.Name, user.UserName, ClaimValueTypes.String, _jwtBearerConfiguration.Value.Issuer),
                new Claim("DisplayName", string.Format("{0} {1}", user.FirstName, user.LastName) , ClaimValueTypes.String, _jwtBearerConfiguration.Value.Issuer),
                // to invalidate the cookie
               // new Claim(ClaimTypes.SerialNumber, user.SerialNumber, ClaimValueTypes.String, _jwtBearerConfiguration.Value.Issuer),
                // custom data
                new Claim(ClaimTypes.UserData, user.UserId, ClaimValueTypes.String, _jwtBearerConfiguration.Value.Issuer)
            };

            // add roles

            foreach (var roleName in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleName, ClaimValueTypes.String, _jwtBearerConfiguration.Value.Issuer));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtBearerConfiguration.Value.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: _jwtBearerConfiguration.Value.Issuer,
                audience: _jwtBearerConfiguration.Value.Audience,
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_jwtBearerConfiguration.Value.AccessTokenExpirationMinutes),
                signingCredentials: creds);
            return (new JwtSecurityTokenHandler().WriteToken(token), claims);
        }
    }
}
