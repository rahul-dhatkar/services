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
//** Created   : 27-Jun-18                                                                 **
//** Purpose   : Password Helper                                                           **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      27-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using CryptoHelper;

namespace Contesto.V2.Core.Common.Utility.Cryptography
{
    /// <summary>
    /// Password Helper
    /// </summary>
    public class PasswordHelper
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static PasswordHelper _instance = null;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static PasswordHelper Instance => _instance ?? new PasswordHelper();

        /// <summary>
        /// Prevents a default instance of the <see cref="PasswordHelper"/> class from being created.
        /// </summary>
        private PasswordHelper()
        {
        }

        // Hash a password
        /// <summary>
        /// Hashes the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }

        // Verify the password hash against the given password
        /// <summary>
        /// Verifies the password.
        /// </summary>
        /// <param name="hashPassword">The hash password.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public bool VerifyPassword(string hashPassword, string password)
        {
            return Crypto.VerifyHashedPassword(hashPassword, password);
        }
    }
}