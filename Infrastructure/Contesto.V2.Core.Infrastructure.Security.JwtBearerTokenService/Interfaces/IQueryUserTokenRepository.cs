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
//** Purpose   : IQueryUserTokenRepository                                                 **
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
using System.Collections.Generic;

namespace Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService.Interfaces
{
    /// <summary>
    /// Interface Query UserToken Repository
    /// </summary>
    /// <seealso cref="IQueryGenericRepository{UserTokenDomainModel}" />
    public interface IQueryUserTokenRepository //: IQueryGenericRepository<UserTokenDomainModel>
    {
        /// <summary>
        /// Gets the by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<UserTokenDomainModel> GetByUserId(string userId);
    }
}
