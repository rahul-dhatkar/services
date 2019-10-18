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
//** Purpose   : Http 403 Result                                                           **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      12-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contesto.V2.Core.Common.Api.OperationFilters
{
    /// <summary>
    /// Http403Result
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ActionResult" />
    public class Http403Result : ActionResult
    {
        /// <summary>
        /// Executes the result operation of the action method synchronously. This method is called by MVC to process
        /// the result of an action method.
        /// </summary>
        /// <param name="context">The context in which the result is executed. The context information includes
        /// information about the action that was executed and request information.</param>
        public override void ExecuteResult(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = 403;
            context.HttpContext.Response.WriteAsync(
                "Sorry You not authorized to call this service. We have captured the caller details. Please contact our support team.");
        }
    }
}