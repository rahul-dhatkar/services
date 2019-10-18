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
//** Author    : Dhiraj G                                                                  **
//** Created   : 20-06-18                                                                  **
//** Purpose   : ITokenStoreService                                                        **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      05-08-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------
using Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService.Dtos;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService.Interfaces
{
    /// <summary>
    /// Interface TokenStore Service
    /// </summary>
    public interface ITokenStoreService
    {
        /// <summary>
        /// Adds the user token asynchronous.
        /// </summary>
        /// <param name="userToken">The user token.</param>
        /// <returns></returns>
        Task AddUserTokenAsync(UserTokenDomainModel userToken);
        /// <summary>
        /// Adds the user token asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="refreshTokenSource">The refresh token source.</param>
        /// <returns></returns>

        Task AddUserTokenAsync(UserDomainModel user, string refreshToken, string accessToken, string refreshTokenSource);
        /// <summary>
        /// Determines whether [is valid token asynchronous] [the specified access token].
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>

        Task<bool> IsValidTokenAsync(string accessToken, string userId);
        /// <summary>
        /// Deletes the expired tokens asynchronous.
        /// </summary>
        /// <returns></returns>

        Task DeleteExpiredTokensAsync();
        /// <summary>
        /// Finds the token asynchronous.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns></returns>

        Task<UserTokenDomainModel> FindTokenAsync(string refreshToken);
        /// <summary>
        /// Deletes the token asynchronous.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns></returns>

        Task DeleteTokenAsync(string refreshToken);
        /// <summary>
        /// Deletes the tokens with same refresh token source asynchronous.
        /// </summary>
        /// <param name="refreshTokenSource">The refresh token source.</param>
        /// <returns></returns>
        Task DeleteTokensWithSameRefreshTokenSourceAsync(string refreshTokenSource);
        /// <summary>
        /// Invalidates the user tokens asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>

        Task InvalidateUserTokensAsync(string userId);
        /// <summary>
        /// Creates the JWT tokens.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="refreshTokenSource">The refresh token source.</param>
        /// <returns></returns>
        Task<(string accessToken, string refreshToken, IEnumerable<Claim> Claims)> CreateJwtTokens(UserDomainModel user, string refreshTokenSource);
        
        /// <summary>
        /// Revokes the user bearer tokens asynchronous.
        /// </summary>
        /// <param name="userIdValue">The user identifier value.</param>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns></returns>
        Task RevokeUserBearerTokensAsync(string userIdValue, string refreshToken);

        /// <summary>
        /// Renews the token.
        /// </summary>
        /// <param name="userToken">The user token.</param>
        /// <returns></returns>
        Task RenewToken(UserTokenDomainModel userToken);
    }
}
