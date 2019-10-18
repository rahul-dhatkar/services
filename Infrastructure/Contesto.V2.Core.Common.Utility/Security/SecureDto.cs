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
//** Purpose   : Secure Dto                                                                **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      27-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

namespace Contesto.V2.Core.Infrastructures.Utility.Security
{
    /// <summary>
    /// Secure Dto
    /// </summary>
    public class SecureDto
    {
        /// <summary>
        /// Gets or sets the message data.
        /// </summary>
        /// <value>
        /// The message data.
        /// </value>
        public string MessageData { get; set; }

        /// <summary>
        /// Gets or sets the file security key.
        /// </summary>
        /// <value>
        /// The file security key.
        /// </value>
        public string FileSecurityKey { get; set; }

        /// <summary>
        /// Gets or sets the security key suffix.
        /// </summary>
        /// <value>
        /// The security key suffix.
        /// </value>
        public string SecurityKeySuffix { get; set; }
    }
}