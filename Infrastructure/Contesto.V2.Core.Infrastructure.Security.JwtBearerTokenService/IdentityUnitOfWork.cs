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
//** Purpose   : IdentityUnitOfWork                                                        **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      08-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService.Interfaces;

namespace Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService
{
    /// <summary>
    /// Identity UnitOfWork
    /// </summary>
    /// <seealso cref="IIdentityUnitOfWork" />
    public class IdentityUnitOfWork : IIdentityUnitOfWork
    {
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityUnitOfWork"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public IdentityUnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Gets or sets the query user token repository.
        /// </summary>
        /// <value>
        /// The query user token repository.
        /// </value>
        public IQueryUserTokenRepository QueryUserTokenRepository { get => new QueryUserTokenRepository(_connectionString); }

        /// <summary>
        /// Gets or sets the command user token repository.
        /// </summary>
        /// <value>
        /// The command user token repository.
        /// </value>
        public ICommandUserTokenRepository CommandUserTokenRepository { get => new CommandUserTokenRepository(_connectionString); }
    }
}
