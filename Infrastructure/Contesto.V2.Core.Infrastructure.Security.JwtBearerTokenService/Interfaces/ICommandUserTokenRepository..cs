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
//** Purpose   : ICommandUserTokenRepository                                               **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      08-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService.Dtos;
using Contesto.V2.Core.Infrastructure.Data.Interfaces;
using System.Threading.Tasks;

namespace Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService.Interfaces
{
    /// <summary>
    /// Interface CommandUserTokenRepository
    /// </summary>
    /// <seealso cref="ICommandGenericRepository{UserTokenDomainModel, System.Int64}" />
    public interface ICommandUserTokenRepository //: ICommandGenericRepository<UserTokenDomainModel, long>
    {
        Task<long> CreateTokens(UserTokenDomainModel model);
        Task<long> UpdateTokens(UserTokenDomainModel model);
        /// <summary>
        /// Deletes the expired tokens.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="refreshTokenSource">The refresh token source.</param>
        /// <returns></returns>
        Task<bool> DeleteExpiredTokens( string userId, string refreshToken, string refreshTokenSource);
    }
}
