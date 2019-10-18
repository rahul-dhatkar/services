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
//** Purpose   : UserTokenDomainModel                                                      **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      05-08-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------
using System;

namespace Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService.Dtos
{
    /// <summary>
    /// User Token DomainModel
    /// </summary>
    public class UserTokenDomainModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the access token hash.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the access token expires date time.
        /// </summary>
        /// <value>
        /// The access token expires date time.
        /// </value>
        public DateTimeOffset AccessTokenExpiresDateTime { get; set; }

        /// <summary>
        /// Gets or sets the refresh token identifier hash.
        /// </summary>
        /// <value>
        /// The refresh token identifier.
        /// </value>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the refresh token identifier hash source.
        /// </summary>
        /// <value>
        /// The refresh token identifier hash source.
        /// </value>
        public string RefreshTokenSource { get; set; }

        /// <summary>
        /// Gets or sets the refresh token expires date time.
        /// </summary>
        /// <value>
        /// The refresh token expires date time.
        /// </value>
        public DateTimeOffset RefreshTokenExpiresDateTime { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }
    }
}
