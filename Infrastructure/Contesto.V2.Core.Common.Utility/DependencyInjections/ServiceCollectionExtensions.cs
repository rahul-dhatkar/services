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
//** Purpose   : Service Collection Extensions                                             **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      27-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using System;

namespace Contesto.V2.Core.Common.Utility.DependencyInjections
{
    /// <summary>
    /// Service Collection Extensions
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the singleton factory.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TFactory">The type of the factory.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        public static IServiceCollection AddSingletonFactory<T, TFactory>(this IServiceCollection collection)
            where T : class where TFactory : class, IServiceFactory<T>
        {
            collection.AddTransient<TFactory>();
            return AddInternal<T, TFactory>(collection, p => p.GetRequiredService<TFactory>(), ServiceLifetime.Singleton);
        }

        /// <summary>
        /// Adds the singleton factory.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TFactory">The type of the factory.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="factory">The factory.</param>
        /// <returns></returns>
        public static IServiceCollection AddSingletonFactory<T, TFactory>(this IServiceCollection collection, TFactory factory)
            where T : class where TFactory : class, IServiceFactory<T>
        {
            return AddInternal<T, TFactory>(collection, p => factory, ServiceLifetime.Singleton);
        }

        /// <summary>
        /// Adds the transient factory.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TFactory">The type of the factory.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        public static IServiceCollection AddTransientFactory<T, TFactory>(this IServiceCollection collection)
            where T : class where TFactory : class, IServiceFactory<T>
        {
            collection.AddTransient<TFactory>();
            return AddInternal<T, TFactory>(collection, p => p.GetRequiredService<TFactory>(), ServiceLifetime.Transient);
        }

        /// <summary>
        /// Adds the transient factory.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TFactory">The type of the factory.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="factory">The factory.</param>
        /// <returns></returns>
        public static IServiceCollection AddTransientFactory<T, TFactory>(this IServiceCollection collection, TFactory factory)
            where T : class where TFactory : class, IServiceFactory<T>
        {
            return AddInternal<T, TFactory>(collection, p => factory, ServiceLifetime.Transient);
        }

        /// <summary>
        /// Adds the scoped factory.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TFactory">The type of the factory.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        public static IServiceCollection AddScopedFactory<T, TFactory>(this IServiceCollection collection)
            where T : class where TFactory : class, IServiceFactory<T>
        {
            collection.AddTransient<TFactory>();
            return AddInternal<T, TFactory>(collection, p => p.GetRequiredService<TFactory>(), ServiceLifetime.Scoped);
        }

        /// <summary>
        /// Adds the scoped factory.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TFactory">The type of the factory.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="factory">The factory.</param>
        /// <returns></returns>
        public static IServiceCollection AddScopedFactory<T, TFactory>(this IServiceCollection collection, TFactory factory)
            where T : class where TFactory : class, IServiceFactory<T>
        {
            return AddInternal<T, TFactory>(collection, p => factory, ServiceLifetime.Scoped);
        }

        /// <summary>
        /// Adds the internal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TFactory">The type of the factory.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="factoryProvider">The factory provider.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <returns></returns>
        private static IServiceCollection AddInternal<T, TFactory>(
            this IServiceCollection collection,
            Func<IServiceProvider, TFactory> factoryProvider,
            ServiceLifetime lifetime) where T : class where TFactory : class, IServiceFactory<T>
        {
            Func<IServiceProvider, object> factoryFunc = provider =>
            {
                var factory = factoryProvider(provider);
                return factory.Build();
            };
            var descriptor = new ServiceDescriptor(
                typeof(T), factoryFunc, lifetime);
            collection.Add(descriptor);
            return collection;
        }
    }
}