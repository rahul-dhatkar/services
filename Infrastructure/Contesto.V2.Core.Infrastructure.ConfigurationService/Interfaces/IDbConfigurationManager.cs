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
//** Purpose   : I Db Configuration Manager                                                **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      27-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using Contesto.V2.Core.Infrastructure.ConfigurationService.Dtos.ViewModels;
using System.Collections.Generic;

namespace Contesto.V2.Core.Infrastructure.ConfigurationService.Interfaces
{
    /// <summary>
    /// I Db Configuration Manager
    /// </summary>
    public interface IDbConfigurationManager
    {
        /// <summary>
        /// Applications the settings.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string AppSettings(string key);

        //Task<bool> UpdateValue(string key, string  value);

        /// <summary>
        /// Gets all values.
        /// </summary>
        /// <returns></returns>
        List<ConfigurationSettingViewModel> GetAllValues();
    }
}