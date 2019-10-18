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
//** Purpose   : Google OAuth Helper                                                       **
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
    /// Google OAuth Helper
    /// </summary>
    public class GoogleOAuthHelper
    {
        /// <summary>
        /// Validates the oath token verify response.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Validated Oath Token Verified Response From Google</returns>
        public static GoogleOAuthRequestViewModel ValidateOathTokenVerifyResponse(OauthRequestViewModel model)
        {
            var client = new HttpClient();
            var googleReply = client.GetStringAsync(string.Format("https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={0}", model.TokenId)).Result;
            var result = JsonConvert.DeserializeObject<GoogleOAuthRequestViewModel>(googleReply);
            return result;
        }
    }
}