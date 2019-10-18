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
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Contesto.V2.Core.Common.Api.Base
{
    /// <summary>
    /// Base Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    public abstract class BaseApiController : ControllerBase
    {

        /// <summary>
        /// Gets the validation result.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="validator">The validator.</param>
        /// <returns>Validation Result</returns>
        protected ValidationResult GetValidationResult(dynamic model, dynamic validator)
        {
            ValidationResult results = validator.Validate(model);
            return results;
        }

        /// <summary>
        /// Builds the validation error message.
        /// </summary>
        /// <param name="validationResult">The validation result.</param>
        /// <returns>Validation Error Message List</returns>
        protected  List<ErrorModel> BuildValidationErrorMessage(ValidationResult validationResult)
        {
            var errorList = new List<ErrorModel>();
            if (validationResult?.Errors == null) return errorList;

            errorList =
                validationResult.Errors.Select(
                    e => new ErrorModel() { PropertyName = e.PropertyName, Message = e.ErrorMessage }).ToList();

            return errorList;
        }
    }
}