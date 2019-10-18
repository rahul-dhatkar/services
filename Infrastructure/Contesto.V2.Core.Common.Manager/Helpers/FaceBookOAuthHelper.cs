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
//** Created   : 12-Jun-18                                                                 **
//** Purpose   : Face Book OAuth Helper                                                    **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      12-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using Contesto.V2.Core.Common.ViewModel.ViewModels;
using Newtonsoft.Json;
using System.Net.Http;

namespace Contesto.V2.Core.Common.Manager.Helpers
{
    /// <summary>
    /// Face Book OAuth Helper
    /// </summary>
    public class FaceBookOAuthHelper
    {
        /// <summary>
        /// Validates the oath token verify response.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Validated Oath Token Verified Response From Facebook</returns>
        public static FaceBookOAuthRequestViewModel ValidateOathTokenVerifyResponse(OauthRequestViewModel model)
        {
            var client = new HttpClient();
            var facebookReply = client.GetStringAsync(string.Format("https://graph.facebook.com/me?fields=name,first_name,last_name,email,gender,picture&access_token={0}", model.TokenId)).Result;
            var result = JsonConvert.DeserializeObject<FaceBookOAuthRequestViewModel>(facebookReply);
            return result;
        }
    }
}