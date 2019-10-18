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
//** Purpose   : IIdentityUnitOfWork                                                       **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      08-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------


namespace Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService.Interfaces
{
    /// <summary>
    /// Interface Identity UnitOfWork
    /// </summary>
    public interface IIdentityUnitOfWork
    {
        /// <summary>
        /// Gets or sets the query user token repository.
        /// </summary>
        /// <value>
        /// The query user token repository.
        /// </value>
        IQueryUserTokenRepository QueryUserTokenRepository { get; }

        /// <summary>
        /// Gets or sets the command user token repository.
        /// </summary>
        /// <value>
        /// The command user token repository.
        /// </value>
        ICommandUserTokenRepository CommandUserTokenRepository { get; }
    }
}
