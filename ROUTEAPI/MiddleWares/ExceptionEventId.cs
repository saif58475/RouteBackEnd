using Microsoft.Extensions.Logging;

namespace Managment_System.MiddleWares
{
    public class ExceptionEventId
    {
        public static EventId GeneralException = new EventId(400, "Bad Request");
        public static EventId NotFoundException = new EventId(1000, "There is no such an entity.Entity type: {0}, id: {1}");
    }
}
