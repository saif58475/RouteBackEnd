using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;

namespace Managment_System.MiddleWares.Types
{
    public class BusinessException : Exception
    {
        public EventId EventId { get; private set; }
        public IEnumerable<string> Errors { get; set; }

        public static Action<ILogger, T1, T2, Guid, BusinessException> Define<T1, T2>()
        {
            return (logger, arg1, arg2, arg3, exception) =>
            {
                logger.LogWarning(exception.EventId, exception, "Business Exception {message} {error} {errorId}", arg1, arg2, arg3);
            };
        }

        public BusinessException(string message, EventId eventId, string error)
           : base(message)
        {
            EventId = eventId;
            Errors = new List<string>() { error };
        }

        public BusinessException(string message, EventId eventId, IEnumerable<string> errors)
            : base(message)
        {
            EventId = eventId;
            Errors = errors;
        }

        public BusinessException WithData(string name, object value)
        {
            Data[name] = value;
            return this;
        }

    }
}
