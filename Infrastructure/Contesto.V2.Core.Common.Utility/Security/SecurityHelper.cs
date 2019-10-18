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
//** Created   : 04-Jun-18                                                                 **
//** Purpose   : Security Helper                                                           **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      04-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

namespace Contesto.V2.Core.Infrastructures.Utility.Security
{
    /// <summary>
    /// Security Helper
    /// </summary>
    public class SecurityHelper
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static SecurityHelper _instance = null;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static SecurityHelper Instance => _instance ?? new SecurityHelper();

        /// <summary>
        /// Prevents a default instance of the <see cref="SecurityHelper"/> class from being created.
        /// </summary>
        private SecurityHelper()
        {
        }

        /// <summary>
        /// Encrypts the message.
        /// </summary>
        /// <param name="messageData">The message data.</param>
        /// <param name="securityKeySuffix">The security key suffix.</param>
        /// <returns></returns>
        public string EncryptMessage(string messageData, string securityKeySuffix)
        {
            return StringCipher.Encrypt(messageData, securityKeySuffix);
        }

        /// <summary>
        /// Decrypts the message.
        /// </summary>
        /// <param name="messageData">The message data.</param>
        /// <param name="securityKeySuffix">The security key suffix.</param>
        /// <returns></returns>
        public string DecryptMessage(string messageData, string securityKeySuffix)
        {
            return StringCipher.Decrypt(messageData, securityKeySuffix); ;
        }
    }
}