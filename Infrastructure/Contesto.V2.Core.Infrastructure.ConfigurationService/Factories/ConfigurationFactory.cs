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
//** Purpose   : Configuration Factory                                                     **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      06-27-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using Contesto.V2.Core.Common.Utility.DependencyInjections;
using Contesto.V2.Core.Infrastructure.ConfigurationService.Dtos;
using Contesto.V2.Core.Infrastructure.ConfigurationService.Interfaces;
using Microsoft.Extensions.Options;

namespace Contesto.V2.Core.Infrastructure.ConfigurationService.Factories
{
    /// <summary>
    /// Configuration Factory
    /// </summary>
    /// <seealso cref="IServiceFactory{IDbConfigurationManager}" />
    public class ConfigurationFactory : IServiceFactory<IDbConfigurationManager>
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IOptions<ConfigurationConfig> _configurationConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationFactory" /> class.
        /// </summary>
        /// <param name="configurationConfig">The configuration.</param>
        /// <param name="configurationType">Type of the configuration.</param>
        public ConfigurationFactory(IOptions<ConfigurationConfig> configurationConfig,  ConfigurationType configurationType = ConfigurationType.DatabaseSettings)
        {
            _configurationConfig = configurationConfig;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        public IDbConfigurationManager Build()
        {
            return DbConfigurationManager.NewInstance(_configurationConfig);
        }
    }
}