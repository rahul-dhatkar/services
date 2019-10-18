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
//** Purpose   : File Upload Operation                                                     **
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
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;

namespace Contesto.V2.Core.Common.Api.OperationFilters
{
    /// <summary>
    /// File Upload Operation
    /// </summary>
    /// <seealso cref="Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter" />
    public class FileUploadOperation : IOperationFilter
    {
        private const string FormDataMimeType = "multi part/form-data";
        private static readonly string[] FormFilePropertyNames =
            typeof(IFormFile).GetTypeInfo().DeclaredProperties.Select(x => x.Name).ToArray();

        /// <summary>
        /// Applies the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="context">The context.</param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            //if (operation.Parameters == null) return;

            //var result = from a in context.ApiDescription.ParameterDescriptions
            //             join b in operation.Parameters.OfType<NonBodyParameter>()
            //             on a.Name equals b?.Name
            //             where a.ModelMetadata.ModelType == typeof(IFormFile)
            //             select b;


            //result.ToList().ForEach(x =>
            //{
            //    x.In = "formData";
            //    x.Description = "Upload file.";
            //    x.Type = "file";
            //});
        }
    }
}