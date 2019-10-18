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
//** Purpose   : Google Recaptcha ViewModel                                                **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      14-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Contesto.V2.Core.Common.ViewModel.ViewModels
{
    /// <summary>
    /// Google Recaptcha View Model
    /// </summary>
    public class GoogleRecaptchaViewModel
    {
        /// <summary>
        /// Gets or sets the success.
        /// </summary>
        /// <value>
        /// The success.
        /// </value>
        [JsonProperty("success")]
        public string Success { get; set; }

        /// <summary>
        /// Gets or sets the error codes.
        /// </summary>
        /// <value>
        /// The error codes.
        /// </value>
        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}