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
//** Created   : 14-Jun-18                                                                 **
//** Purpose   : I Busines Process                                                         **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      14-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using Contesto.V2.Core.Common.Utility.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contesto.V2.Core.Common.BusinessProcess.Interfaces
{
    /// <summary>
    /// Interface Business Process
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IBusinessProcess<in TModel, TResult>
    {
        /// <summary>
        /// Validates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<List<ErrorModel>> ValidateInput(TModel model);

        /// <summary>
        /// Validates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<List<ErrorModel>> ValidateBusinessRules(TModel model);

        /// <summary>
        /// Executes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        Task<TResult> Execute(TModel model, string userName);
    }
}