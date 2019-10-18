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
//** Created   : 20-06-18                                                                  **
//** Purpose   : ConfigurationServiceExtension                                           **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Nam Team      20-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------


using Contesto.V2.Core.Common.Utility.DependencyInjections;
using Contesto.V2.Core.Infrastructure.ConfigurationService.Dtos;
using Contesto.V2.Core.Infrastructure.ConfigurationService.Factories;
using Contesto.V2.Core.Infrastructure.ConfigurationService.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contesto.V2.Core.Infrastructure.ConfigurationService.Extensions
{
    /// <summary>
    /// Configuration Service Extension
    /// </summary>
    public static class ConfigurationServiceExtension
    {
        /// <summary>
        /// Adds the configuration.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IServiceCollection AddDbConfigurationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConfigurationConfig>(options=> configuration.GetSection("ConfigurationConfig").Bind(options));
            services.AddSingletonFactory<IDbConfigurationManager, ConfigurationFactory>();
            return services;
        }
    }
}
