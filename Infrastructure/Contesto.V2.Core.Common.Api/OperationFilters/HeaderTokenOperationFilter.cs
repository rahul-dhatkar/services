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
//** Purpose   : Header Token Operation Filter                                             **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      12-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;

namespace Contesto.V2.Core.Common.Api.OperationFilters
{
    /// <summary>
    /// HeaderTokenOperationFilter
    /// </summary>
    /// <seealso cref="Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter" />
    public class HeaderTokenOperationFilter : IOperationFilter
    {
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderTokenOperationFilter"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public HeaderTokenOperationFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Applies the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="context">The context.</param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var isAntiforgeryOn = Convert.ToBoolean(_configuration["AppConfiguration:IsAntiforgeryOn"]);

            if (isAntiforgeryOn)
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<IParameter>();

                var headerUserName = new NonBodyParameter { Name = "X-XSRF-TOKEN", Description = "Antiforgery token", In = "header", Required = true, Type = "string" };
                operation.Parameters.Add(headerUserName);
            }
        }
    }
}