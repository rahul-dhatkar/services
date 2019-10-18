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
//** Created   : 08-06-18                                                                  **
//** Purpose   : LoggerProvider                                                            **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      08-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using Contesto.V2.Core.Infrastructure.LoggerService.Dtos;
using Microsoft.Extensions.Logging;
using System;

namespace Contesto.V2.Core.Infrastructure.LoggerService.Providers
{
    /// <summary>
    /// Logger Provider
    /// </summary>
    /// <seealso cref="Microsoft.Extensions.Logging.ILoggerProvider" />
    public class LoggerProvider : ILoggerProvider
    {
        /// <summary>
        /// The filter
        /// </summary>
        private readonly Func<string, LogLevel, bool> _filter;

        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// The logger type
        /// </summary>
        private readonly LoggerTypeEnum _loggerType;

        public LoggerProvider(Func<string, LogLevel, bool> filter, LoggerTypeEnum loggerType, string connectionString)
        {
            _filter = filter;
            _connectionString = connectionString;
            _loggerType = loggerType;
        }

        public ILogger CreateLogger(string categoryName)
        {
            if (_loggerType == LoggerTypeEnum.TextFile)
                throw new NotSupportedException();
            else
                return new DBLoggerManager(categoryName, _filter, _connectionString);
        }

        public void Dispose()
        {

        }
    }
}
