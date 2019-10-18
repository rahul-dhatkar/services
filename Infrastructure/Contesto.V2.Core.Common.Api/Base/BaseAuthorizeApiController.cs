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
//** Purpose   : Base Controller                                                           **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      12-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using Contesto.V2.Core.Common.Utility.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Contesto.V2.Core.Common.Api.Base
{
    /// <summary>
    /// Base Controller
    /// </summary>
    /// <seealso cref="BaseApiController" />
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public abstract class BaseAuthorizeApiController : BaseApiController
    {

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        protected string UserName
        {
            get
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var userName = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                return userName ?? userName;
            }
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        protected string UserId
        {
            get
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var userName = claimsIdentity.FindFirst(ClaimTypes.UserData)?.Value;
                return userName ?? userName;
            }
        }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        protected List<string> Roles
        {
            get
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var userName = claimsIdentity.FindAll(ClaimTypes.Role);
                return userName.Select(x => x.Value).ToList();
            }
        }
    }
}