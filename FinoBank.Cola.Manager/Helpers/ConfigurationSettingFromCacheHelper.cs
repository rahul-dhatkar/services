using Contesto.V2.Core.Infrastructure.ConfigurationService.Dtos.ViewModels;
using Contesto.V2.Core.Infrastructure.ConfigurationService.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinoBank.Cola.Manager.Helpers
{
    /// <summary>
    /// Configuration Setting From Cache Helper
    /// </summary>
    /// <seealso cref="FinoBank.Cola.Api.Helpers.IConfigurationSettingFromCacheHelper" />
    public class ConfigurationSettingFromCacheHelper : IConfigurationSettingFromCacheHelper
    {
        /// <summary>
        /// The cache
        /// </summary>
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// The database configuration manager
        /// </summary>
        private readonly IDbConfigurationManager _dbConfigurationManager;

        /// <summary>
        /// The cache key
        /// </summary>
        private const string cacheKey = "CONFIGURATION_CACHED";

        public ConfigurationSettingFromCacheHelper(IMemoryCache memoryCache,
            IDbConfigurationManager dbConfigurationManager)
        {
            _memoryCache = memoryCache;
            _dbConfigurationManager = dbConfigurationManager;
        }

        /// <summary>
        /// Applications the settings.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string AppSettings(string key)
        {
            var value = GetAllValues().SingleOrDefault(x => x.Key == key);

            return value != null ? value.Value : string.Empty;
        }

        /// <summary>
        /// Gets all values.
        /// </summary>
        private List<ConfigurationSettingViewModel> GetAllValues()
        {
            List<ConfigurationSettingViewModel> configurationSettings;

            if (!_memoryCache.TryGetValue(cacheKey, out configurationSettings))
            {
                configurationSettings = _dbConfigurationManager.GetAllValues();
                // Decide how to cache it
                var opts = new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromSeconds(15)
                };

                // Store it in cache
                _memoryCache.Set(cacheKey, configurationSettings, opts);
            }

            return configurationSettings;
        }
    }

    public interface IConfigurationSettingFromCacheHelper
    {
        string AppSettings(string key);
    }
}