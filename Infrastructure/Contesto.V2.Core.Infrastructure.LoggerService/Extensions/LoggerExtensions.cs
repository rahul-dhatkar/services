using Contesto.V2.Core.Infrastructure.LoggerService.Dtos;
using Contesto.V2.Core.Infrastructure.LoggerService.Providers;
using Microsoft.Extensions.Logging;
using System;

namespace Contesto.V2.Core.Infrastructure.LoggerService.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class LoggerExtensions
    {
        public static ILoggerFactory AddContext(this ILoggerFactory factory, LoggerTypeEnum loggerType,
        Func<string, LogLevel, bool> filter = null, string connectionStr = null)
        {
            factory.AddProvider(new LoggerProvider(filter, loggerType, connectionStr));
            return factory;
        }

        public static ILoggerFactory AddContext(this ILoggerFactory factory, LoggerTypeEnum loggerType, LogLevel minLevel, string connectionStr)
        {
            return AddContext(
                factory, loggerType, (_, logLevel) => logLevel >= minLevel, connectionStr);
        }
    }
}
