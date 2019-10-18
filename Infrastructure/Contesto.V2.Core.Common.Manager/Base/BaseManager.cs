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
//** Purpose   : Base Manager                                                              **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      27-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using AutoMapper;
using Contesto.V2.Core.Common.Manager.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;

namespace Contesto.V2.Core.Common.Manager.Base
{
    /// <summary>
    /// Base Manager
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Manager.Interfaces.IBaseManager" />
    public abstract class BaseManager : IBaseManager
    {
        /// <summary>
        /// The mapp service
        /// </summary>
        protected readonly IMapper MappService;

        /// <summary>
        /// The cache service
        /// </summary>
        protected readonly IMemoryCache CacheService;

        /// <summary>
        /// The file provider service
        /// </summary>
        protected readonly IFileProvider FileProviderService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseManager"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="cache">The cache.</param>
        /// <param name="fileProvider">The file provider.</param>
        protected BaseManager(IMapper mapper,
            IMemoryCache cache, IFileProvider fileProvider)
        {
            MappService = mapper;
            CacheService = cache;
            FileProviderService = fileProvider;
        }
    }
}