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
//** Created   : 27-Jun-18                                                                 **
//** Purpose   : IService Factory                                                          **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      27-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

namespace Contesto.V2.Core.Common.Utility.DependencyInjections
{
    /// <summary>
    /// IService Factory
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IServiceFactory<T> where T : class
    {
        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        T Build();
    }
}