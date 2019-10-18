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
//** Purpose   : DBLoggerManager                                                           **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      08-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using Contesto.V2.Core.Infrastructure.LoggerService.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace Contesto.V2.Core.Infrastructure.LoggerService
{
    /// <summary>
    /// Database Logger
    /// </summary>
    /// <seealso cref="Microsoft.Extensions.Logging.ILogger" />
    internal class DBLoggerManager : ILogger
    {
        /// <summary>
        /// The category name
        /// </summary>
        private readonly string _categoryName;
        /// <summary>
        /// The filter
        /// </summary>
        private readonly Func<string, LogLevel, bool> _filter;
        /// <summary>
        /// The message maximum length
        /// </summary>
        private readonly int MessageMaxLength = 4000;

        /// <summary>
        /// The command logger repository
        /// </summary>
        private readonly ICommandLoggerRepository _commandLoggerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbLogger"/> class.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="connectionString">The connection string.</param>
        public DBLoggerManager(string categoryName, Func<string, LogLevel, bool> filter, string connectionString)
        {
            _categoryName = categoryName;
            _filter = filter;
            _commandLoggerRepository = new CommandLoggerRepository(connectionString);
        }

        /// <summary>
        /// Begins a logical operation scope.
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state">The identifier for the scope.</param>
        /// <returns>
        /// An IDisposable that ends the logical operation scope on dispose.
        /// </returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        /// <summary>
        /// Checks if the given <paramref name="logLevel" /> is enabled.
        /// </summary>
        /// <param name="logLevel">level to be checked.</param>
        /// <returns>
        ///   <c>true</c> if enabled.
        /// </returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return (_filter == null || _filter(_categoryName, logLevel));
        }

        /// <summary>
        /// Writes a log entry.
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="eventId">Id of the event.</param>
        /// <param name="state">The entry to be written. Can be also an object.</param>
        /// <param name="exception">The exception related to this entry.</param>
        /// <param name="formatter">Function to create a <c>string</c> message of the <paramref name="state" /> and <paramref name="exception" />.</param>
        /// <exception cref="System.ArgumentNullException">formatter</exception>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }
            var message = formatter(state, exception);
            var innerExceptionMessage = "";
            if (exception != null && exception.InnerException != null)
            {
                innerExceptionMessage = formatter(state, exception.InnerException);
                innerExceptionMessage += "\n" + exception.InnerException.ToString();
            }

            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            if (exception != null)
            {
                message += "\n" + exception.ToString();

            }

            message = message.Length > MessageMaxLength ? message.Substring(0, MessageMaxLength) : message;
            innerExceptionMessage = innerExceptionMessage.Length > MessageMaxLength ? innerExceptionMessage.Substring(0, MessageMaxLength) : innerExceptionMessage;

            var stackTrace = (exception != null) ? exception.StackTrace : string.Empty;
            Dtos.LoggerDomainModel model = new Dtos.LoggerDomainModel
            {
                Message = message,
                EventId = eventId.Id,
                LogLevel = logLevel.ToString(),
                InnerExceptionMessage = innerExceptionMessage,
                StackTrace = stackTrace
            };

            _commandLoggerRepository.Create(model);
        }
    }
}
