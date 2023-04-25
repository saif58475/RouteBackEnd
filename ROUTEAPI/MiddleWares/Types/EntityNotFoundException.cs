using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;

namespace Managment_System.MiddleWares.Types
{
    public class EntityNotFoundException : Exception
    {
        public Type EntityType { get; set; }

        /// <summary>
        /// Id of the Entity.
        /// </summary>
        public object Id { get; set; }

        public EventId EventId { get; } = ExceptionEventId.NotFoundException;

        public static Action<ILogger, T1, Guid, EntityNotFoundException> Define<T1>()
        {
            return (logger, arg1, arg2, exception) =>
            {
                logger.LogWarning(exception.EventId, exception, "EntityNotFound Exception {message} {errorId}", arg1, arg2);
            };
        }

        public EntityNotFoundException(EventId eventId, Type entityType, object id)
           : base(string.Format(eventId.Name, entityType.FullName, id))
        {
            EventId = eventId;
        }

        public EntityNotFoundException(string message, EventId eventId)
           : base(message)
        {
            EventId = eventId;
        }

        public EntityNotFoundException(string message, EventId eventId, IEnumerable<string> errors)
            : base(message)
        {
            EventId = eventId;
        }


    }
}
