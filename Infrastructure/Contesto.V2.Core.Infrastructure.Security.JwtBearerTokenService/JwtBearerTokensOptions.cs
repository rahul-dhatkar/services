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
//** Purpose   : JwtBearerTokensOptions                                                    **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      05-08-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------
namespace Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService
{
    /// <summary>
    /// Jwt Bearer Tokens Options
    /// </summary>
    public class JwtBearerTokensOptions
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { set; get; }

        /// <summary>
        /// Gets or sets the issuer.
        /// </summary>
        /// <value>
        /// The issuer.
        /// </value>
        public string Issuer { set; get; }

        /// <summary>
        /// Gets or sets the audience.
        /// </summary>
        /// <value>
        /// The audience.
        /// </value>
        public string Audience { set; get; }

        /// <summary>
        /// Gets or sets the access token expiration minutes.
        /// </summary>
        /// <value>
        /// The access token expiration minutes.
        /// </value>
        public int AccessTokenExpirationMinutes { set; get; }

        /// <summary>
        /// Gets or sets the refresh token expiration minutes.
        /// </summary>
        /// <value>
        /// The refresh token expiration minutes.
        /// </value>
        public int RefreshTokenExpirationMinutes { set; get; }

        /// <summary>
        /// Gets or sets a value indicating whether [allow multiple logins from the same user].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow multiple logins from the same user]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowMultipleLoginsFromTheSameUser { set; get; }

        /// <summary>
        /// Gets or sets a value indicating whether [allow signout all user active clients].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow signout all user active clients]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowSignoutAllUserActiveClients { set; get; }
    }
}
