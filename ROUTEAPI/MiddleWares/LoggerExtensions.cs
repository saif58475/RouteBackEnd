using Managment_System.MiddleWares.Types;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace Managment_System.MiddleWares
{
    public static class LoggerExtensions
    {
        private static readonly Action<ILogger, string, Guid, Exception> _generalException;
        private static readonly Action<ILogger, string, string, Guid, BusinessException> _businessException;
        private static readonly Action<ILogger, string, Guid, EntityNotFoundException> _entityNotFoundException;

        static LoggerExtensions()
        {
            _generalException = LoggerMessage.Define<string, Guid>(
                logLevel: LogLevel.Error,
                eventId: ExceptionEventId.GeneralException,
                formatString: "General Exception error {message} {errorId}"
                );

            _businessException = BusinessException.Define<string, string>();
            _entityNotFoundException = EntityNotFoundException.Define<string>();
        }

        public static void ApiGeneralExceptionLogging(this ILogger logger, Exception ex, Guid apiErrorId)
        {
            _generalException(logger, ex.Message, apiErrorId, ex);
        }

        public static void ApiBusinessExceptionLogging(this ILogger logger, BusinessException ex, Guid apiErrorId)
        {
            var error = JsonConvert.SerializeObject(ex.Errors);
            _businessException(logger, ex.Message, error, apiErrorId, ex);
        }

        public static void ApiEntityNotFoundExceptionLogging(this ILogger logger, EntityNotFoundException ex, Guid apiErrorId)
        {
            _entityNotFoundException(logger, ex.Message, apiErrorId, ex);
        }
    }
}
