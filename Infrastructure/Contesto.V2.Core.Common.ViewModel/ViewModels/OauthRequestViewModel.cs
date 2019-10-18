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
//** Created   : 14-Jun-18                                                                 **
//** Purpose   : Oauth Request Model                                                       **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      14-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

namespace Contesto.V2.Core.Common.ViewModel.ViewModels
{
    /// <summary>
    /// Oauth Request Model
    /// </summary>
    public class OauthRequestViewModel
    {
        /// <summary>
        /// Gets or sets the type of the oauth.
        /// </summary>
        /// <value>
        /// The type of the oauth.
        /// </value>
        public string OauthType { get; set; }

        /// <summary>
        /// Gets or sets the token identifier.
        /// </summary>
        /// <value>
        /// The token identifier.
        /// </value>
        public string TokenId { get; set; }
    }
}