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
//** Purpose   : TokenValidatorService                                                    **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      05-08-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Linq;

namespace Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService
{
    /// <summary>
    /// TokenValidator Service
    /// </summary>
    /// <seealso cref="ITokenValidatorService" />
    public class TokenValidatorService : ITokenValidatorService
    {
        /// <summary>
        /// The token store service
        /// </summary>
        private readonly ITokenStoreService _tokenStoreService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenValidatorService" /> class.
        /// </summary>
        /// <param name="tokenStoreService">The token store service.</param>
        public TokenValidatorService(ITokenStoreService tokenStoreService)
        {
            _tokenStoreService = tokenStoreService;
        }

        public async Task RenewToken(TokenValidatedContext context)
        {
            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
            if (claimsIdentity?.Claims == null || !claimsIdentity.Claims.Any())
            {
                context.Fail("This is not our issued token. It has no claims.");
                return;
            }
            var userIdString = claimsIdentity.FindFirst(ClaimTypes.UserData).Value;
            var accessToken = context.SecurityToken as JwtSecurityToken;
            await _tokenStoreService.RenewToken(new Dtos.UserTokenDomainModel() { AccessToken = accessToken.RawData, UserId = userIdString });
        }

        /// <summary>
        /// Validates the asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public async Task ValidateAsync(TokenValidatedContext context)
        {
            var userPrincipal = context.Principal;

            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
            if (claimsIdentity?.Claims == null || !claimsIdentity.Claims.Any())
            {
                context.Fail("This is not our issued token. It has no claims.");
                return;
            }

            //var serialNumberClaim = claimsIdentity.FindFirst(ClaimTypes.SerialNumber);
            //if (serialNumberClaim == null)
            //{
            //    context.Fail("This is not our issued token. It has no serial.");
            //    return;
            //}

            var userIdString = claimsIdentity.FindFirst(ClaimTypes.UserData).Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                context.Fail("This is not our issued token. It has no user-id.");
                return;
            }

            //var user = await _usersService.FindUserAsync(userId);
            //if (user == null || user.SerialNumber != serialNumberClaim.Value || !user.IsActive)
            //{
            //    // user has changed his/her password/roles/stat/IsActive
            //    context.Fail("This token is expired. Please login again.");
            //}

            var accessToken = context.SecurityToken as JwtSecurityToken;
            if (accessToken == null || string.IsNullOrWhiteSpace(accessToken.RawData) ||
                !await _tokenStoreService.IsValidTokenAsync(accessToken.RawData, userIdString))
            {
                context.Fail("This token is not in our database.");
                return;
            }
            await RenewToken(context);
        }
    }
}
