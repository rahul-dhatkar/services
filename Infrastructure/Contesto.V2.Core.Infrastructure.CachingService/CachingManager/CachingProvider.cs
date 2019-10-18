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
//** Purpose   : Caching Provider                                                          **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      27-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using Microsoft.Extensions.Caching.Distributed;

namespace Contesto.V2.Core.Infrastructure.CachingService.CachingManager
{
    /// <summary>
    /// Caching Provider
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Infrastructure.CachingService.CachingManager.CachingProviderBase" />
    /// <seealso cref="Contesto.V2.Core.Infrastructure.CachingService.CachingManager.ICachingProvider" />
    public class CachingProvider : CachingProviderBase, ICachingProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CachingProvider"/> class.
        /// </summary>
        /// <param name="distributedCache">The distributed cache.</param>
        protected CachingProvider(IDistributedCache distributedCache) : base(distributedCache)
        {
        }

        #region ICachingProvider

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public virtual new void AddItem(string key, object value)
        {
            base.AddItem(key, value);
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public virtual object GetItem(string key)
        {
            return base.GetItem(key, false);
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="remove">if set to <c>true</c> [remove].</param>
        /// <returns></returns>
        public virtual new object GetItem(string key, bool remove)
        {
            return base.GetItem(key, remove);
        }

        #endregion ICachingProvider
    }
}