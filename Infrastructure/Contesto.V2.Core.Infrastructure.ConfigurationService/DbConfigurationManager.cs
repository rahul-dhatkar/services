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
//** Purpose   : Db Configuration Manager                                                  **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      06-27-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using Contesto.V2.Core.Infrastructure.ConfigurationService.Dtos;
using Contesto.V2.Core.Infrastructure.ConfigurationService.Dtos.ViewModels;
using Contesto.V2.Core.Infrastructure.ConfigurationService.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
namespace Contesto.V2.Core.Infrastructure.ConfigurationService
{
    /// <summary>
    /// Db Configuration Manager
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Infrastructure.ConfigurationService.Interfaces.IDbConfigurationManager" />
    public class DbConfigurationManager : IDbConfigurationManager
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IQueryConfigurationRepository _repository;

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IOptions<ConfigurationConfig> _configurationConfig;

        /// <summary>
        /// The configuration settings
        /// </summary>
        private readonly List<ConfigurationSettingViewModel> _configurationSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbConfigurationManager" /> class.
        /// </summary>
        /// <param name="configurationConfig">The configuration.</param>
        public DbConfigurationManager(IOptions<ConfigurationConfig> configurationConfig)
        {
            if (!string.IsNullOrEmpty(configurationConfig.Value.DbConnectionString))
            {
                _configurationConfig = configurationConfig;
                _repository = new QueryConfigurationRepository(configurationConfig.Value.DbConnectionString);
                _configurationSettings = ReadConfigurationSetting().Result;
            }
        }

        /// <summary>
        /// News the instance.
        /// </summary>
        /// <param name="configurationConfig">The configuration.</param>
        /// <returns></returns>
        public static DbConfigurationManager NewInstance(IOptions<ConfigurationConfig> configurationConfig)
        {
            return new DbConfigurationManager(configurationConfig);
        }

        /// <summary>
        /// Applications the settings.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string AppSettings(string key)
        {
            var result = _configurationSettings.FirstOrDefault(x => x.Environment == _configurationConfig.Value.RunTimeEnvironment && x.IsActive && x.Key == key);
            return result != null ? result.Value : string.Empty;
        }

        /// <summary>
        /// Gets all values.
        /// </summary>
        /// <returns></returns>
        public List<ConfigurationSettingViewModel> GetAllValues()
        {
            return _configurationSettings.Where(x => x.Environment == _configurationConfig.Value.RunTimeEnvironment && x.IsActive).ToList();
        }

        /// <summary>
        /// Reads the configuration setting.
        /// </summary>
        /// <returns></returns>
        private async Task<List<ConfigurationSettingViewModel>> ReadConfigurationSetting()
        {
            //TODO add Caching for 15 mins
            var dbResults = await _repository.GetAllConfiguration().ConfigureAwait(false);
            var results = new List<ConfigurationSettingViewModel>();
            dbResults.ForEach(x =>
            {
                results.Add(new ConfigurationSettingViewModel()
                {
                    Environment = x.Environment,
                    Key = x.Key,
                    Value = x.Value,
                    IsActive = x.IsActive
                });
            });
            return results;
        }
    }
}