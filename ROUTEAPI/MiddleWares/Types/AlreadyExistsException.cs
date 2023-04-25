using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Managment_System.MiddleWares.Types
{
    public class AlreadyExistsException : BusinessException
    {
        public AlreadyExistsException(string value, EventId errorCode)
            : base(string.Format(errorCode.Name, value), errorCode, string.Format(errorCode.Name, value))
        {
            WithData(nameof(value), value);
        }

        public AlreadyExistsException(string value, EventId errorCode, IEnumerable<string> errors)
            : base(string.Format(errorCode.Name, value), errorCode, errors)
        {
            WithData(nameof(value), value);
        }
    }
}
