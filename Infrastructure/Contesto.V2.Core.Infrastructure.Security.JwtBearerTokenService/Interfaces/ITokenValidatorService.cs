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
//** Purpose   : ITokenValidatorService                                                    **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      05-08-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;

namespace Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService.Interfaces
{
    /// <summary>
    /// Interface TokenValidatorService
    /// </summary>
    public interface ITokenValidatorService
    {
        /// <summary>
        /// Validates the asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        Task ValidateAsync(TokenValidatedContext context);

        /// <summary>
        /// Renews the token.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        Task RenewToken(TokenValidatedContext context);
    }
}
