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
//** Purpose   : Caching Provider Base                                                     **
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
    /// Caching Provider Base
    /// </summary>
    public abstract class CachingProviderBase
    {
        /// <summary>
        /// The distributed cache
        /// </summary>
        protected readonly IDistributedCache _distributedCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="CachingProviderBase"/> class.
        /// </summary>
        /// <param name="distributedCache">The distributed cache.</param>
        public CachingProviderBase(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public virtual void AddItem(string key, object value)
        {
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public virtual object GetItem(string key)
        {
            return new object();
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="remove">if set to <c>true</c> [remove].</param>
        /// <returns></returns>
        public virtual object GetItem(string key, bool remove)
        {
            return new object();
        }
    }
}