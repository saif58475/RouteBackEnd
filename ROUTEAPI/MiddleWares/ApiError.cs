using Managment_System.MiddleWares.Types;
using Microsoft.Data.SqlClient;
using System;
using System.Net;


namespace Managment_System.MiddleWares
{
    public class ApiError
    {
        public Guid Id { get; private set; }
        public HttpStatusCode MessageCode { get; private set; }
        //public EventId EventId { get; private set; }
        public string Message { get; private set; }
        public bool Success { get; private set; } = false;
        public dynamic Data { get; set; }

        private ApiError() { }

        public ApiError(string message, HttpStatusCode statusCode)
        {
            Id = Guid.NewGuid();
            this.Message = message;
            MessageCode = statusCode;
            Success = false;
        }

        public static ApiError CreateGeneralException(Exception exception)
        {
            var innerException = exception.InnerException as SqlException;
            if (innerException != null && Equals(innerException.Number, 547))
                return new ApiError("عفوا لا يمكن اتمام العملية بسبب وجود ارتباطات", HttpStatusCode.BadRequest);
            return new ApiError(exception.Message, HttpStatusCode.BadRequest);
        }

        public static ApiError CreateBusinessException(BusinessException businessException)
        {
            return new ApiError(businessException.Message, HttpStatusCode.UnprocessableEntity);
        }

        public static ApiError CreateEntityNotFoundException(EntityNotFoundException entityNotFoundException)
        {
            return new ApiError(entityNotFoundException.Message, HttpStatusCode.NotFound);
        }
    }

}
