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
//** Purpose   : AntiForgeryCookieService                                                 **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      05-08-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using Contesto.V2.Core.Infrastructure.Security.AntiForgeryTokenService.Interfaces;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Security.Claims;

namespace Contesto.V2.Core.Infrastructure.Security.AntiForgeryTokenService
{
    /// <summary>
    /// AntiForgery Cookie Service
    /// </summary>
    /// <seealso cref="IAntiForgeryCookieService" />
    public class AntiForgeryCookieService : IAntiForgeryCookieService
    {
        private const string XsrfTokenKey = "XSRF-TOKEN";

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IAntiforgery _antiforgery;
        private readonly IOptions<AntiforgeryOptions> _antiforgeryOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="AntiForgeryCookieService"/> class.
        /// </summary>
        /// <param name="contextAccessor">The context accessor.</param>
        /// <param name="antiforgery">The antiforgery.</param>
        /// <param name="antiforgeryOptions">The antiforgery options.</param>
        public AntiForgeryCookieService(
            IHttpContextAccessor contextAccessor,
            IAntiforgery antiforgery,
            IOptions<AntiforgeryOptions> antiforgeryOptions)
        {
            _contextAccessor = contextAccessor;
            _antiforgery = antiforgery;
            _antiforgeryOptions = antiforgeryOptions;
        }

        /// <summary>
        /// Regenerates the anti forgery cookies.
        /// </summary>
        /// <param name="claims">The claims.</param>
        public void RegenerateAntiForgeryCookies(IEnumerable<Claim> claims)
        {
            var httpContext = _contextAccessor.HttpContext;
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme));
            var tokens = _antiforgery.GetAndStoreTokens(httpContext);
            httpContext.Response.Cookies.Append(
                  key: XsrfTokenKey,
                  value: tokens.RequestToken,
                  options: new CookieOptions
                  {
                      HttpOnly = false // Now JavaScript is able to read the cookie
                  });
        }

        /// <summary>
        /// Deletes the anti forgery cookies.
        /// </summary>
        public void DeleteAntiForgeryCookies()
        {
            var cookies = _contextAccessor.HttpContext.Response.Cookies;
            cookies.Delete(_antiforgeryOptions.Value.Cookie.Name);
            cookies.Delete(XsrfTokenKey);
        }
    }
}
