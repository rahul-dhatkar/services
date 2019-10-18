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
//** Purpose   : IAntiForgeryCookieService                                                 **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      05-08-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Security.Claims;

namespace Contesto.V2.Core.Infrastructure.Security.AntiForgeryTokenService.Interfaces
{
    /// <summary>
    /// Interface Anti Forgery Cookie Service
    /// </summary>
    public interface IAntiForgeryCookieService
    {
        /// <summary>
        /// Regenerates the anti forgery cookies.
        /// </summary>
        /// <param name="claims">The claims.</param>
        void RegenerateAntiForgeryCookies(IEnumerable<Claim> claims);

        /// <summary>
        /// Deletes the anti forgery cookies.
        /// </summary>
        void DeleteAntiForgeryCookies();
    }
}
