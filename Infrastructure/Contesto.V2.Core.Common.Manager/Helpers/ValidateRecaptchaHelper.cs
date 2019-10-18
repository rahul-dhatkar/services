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
//** Purpose   : Validate Recaptcha Helper                                                 **
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
using System.Net.Http;
using Contesto.V2.Core.Common.Utility.Models;
using Contesto.V2.Core.Common.ViewModel.ViewModels;

namespace Contesto.V2.Core.Common.Manager.Helpers
{
    /// <summary>
    /// Validate Recaptcha Helper
    /// </summary>
    public class ValidateRecaptchaHelper
    {
        /// <summary>
        /// Validates the recaptcha verify response.
        /// </summary>
        /// <param name="privateKey">The private key.</param>
        /// <param name="recaptchaVerifyResponse">The recaptcha verify response.</param>
        /// <returns></returns>
        public static List<ErrorModel> ValidateRecaptchaVerifyResponse(string privateKey, string recaptchaVerifyResponse)
        {
            var client = new HttpClient();
            var googleReply = client.GetStringAsync(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", privateKey, recaptchaVerifyResponse)).Result;
            var result = JsonConvert.DeserializeObject<GoogleRecaptchaViewModel>(googleReply);
            var errorList = new List<ErrorModel>();
            if (result.Success != "true")
                errorList.Add(new ErrorModel() { PropertyName = "RecaptchaVerifyResponse", Message = "Invaild rCAPTCHA Verification" });

            return errorList;
        }
    }
}