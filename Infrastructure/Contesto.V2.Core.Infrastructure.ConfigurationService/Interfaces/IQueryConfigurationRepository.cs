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
//** Created   : 06-27-18                                                                 **
//** Purpose   : Interface Configuration Repository                                                **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      06-27-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using Contesto.V2.Core.Infrastructure.ConfigurationService.Dtos.DomainModels;
using Contesto.V2.Core.Infrastructure.Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contesto.V2.Core.Infrastructure.ConfigurationService.Interfaces
{
    /// <summary>
    /// Interface Query Configuration Repository
    /// </summary>
    internal interface IQueryConfigurationRepository : IQueryGenericSqlRepository<ConfigurationSettingDomainModel>
    {
        Task<List<ConfigurationSettingDomainModel>> GetAllConfiguration(string searchTxt = null);
    }
}