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
//** Created   : 07-Jun-18                                                                 **
//** Purpose   : Response Builder Helper                                                   **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      07-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using Contesto.V2.Core.Common.Manager.Results;
using System;
using System.Collections.Generic;
using Contesto.V2.Core.Common.Utility.Models;

namespace Contesto.V2.Core.Common.Manager.Helpers
{
    /// <summary>
    /// Response Builder Helper
    /// </summary>
    public class ResponseBuilderHelper
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static ResponseBuilderHelper _instance = null;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static ResponseBuilderHelper Instance => _instance ?? new ResponseBuilderHelper();

        /// <summary>
        /// Prevents a default instance of the <see cref="ResponseBuilderHelper"/> class from being created.
        /// </summary>
        private ResponseBuilderHelper() { }

        /// <summary>
        /// Builds the sucess result.
        /// </summary>
        /// <returns></returns>
        public OperationResult BuildSucessResult()
        {
            return new OperationResult { OperationId = Guid.NewGuid().ToString("D"), Success = true };
        }

        /// <summary>
        /// Builds the un sucess result.
        /// </summary>
        /// <param name="errorMessages">The error messages.</param>
        /// <returns></returns>
        public OperationResult BuildUnSucessResult(List<ErrorModel> errorMessages)
        {
            return new OperationResult { OperationId = Guid.NewGuid().ToString("D"), Success = false, ErrorMessages = errorMessages };
        }
    }

    /// <summary>
    /// Response Builder Helper
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseBuilderHelper<T> where T : class
    {
        private static ResponseBuilderHelper<T> _instance = null;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static ResponseBuilderHelper<T> Instance => _instance ?? new ResponseBuilderHelper<T>();

        /// <summary>
        /// Builds the sucess result.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public OperationResult<T> BuildSucessResult(T model)
        {
            return new OperationResult<T> { OperationId = Guid.NewGuid().ToString("D"), Success = true, Data = model };
        }

        /// <summary>
        /// Builds the un sucess result.
        /// </summary>
        /// <param name="errorMessages">The error messages.</param>
        /// <returns></returns>
        public OperationResult<T> BuildUnSucessResult(List<ErrorModel> errorMessages)
        {
            return new OperationResult<T> { OperationId = Guid.NewGuid().ToString("D"), Success = false, ErrorMessages = errorMessages };
        }
    }
}