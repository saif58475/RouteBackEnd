using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using System.Text.Json;
using Managment_System.MiddleWares.Types;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Managment_System.MiddleWares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            ApiError apiError = null;

            while (exception.GetType().IsEquivalentTo(typeof(AggregateException)))
                exception = exception.InnerException;

            switch (exception)
            {
                case BusinessException ex:
                    apiError = ApiError.CreateBusinessException(ex);
                    _logger.ApiBusinessExceptionLogging(ex, apiError.Id);
                    break;
                case EntityNotFoundException ex:
                    apiError = ApiError.CreateEntityNotFoundException(ex);
                    _logger.ApiEntityNotFoundExceptionLogging(ex, apiError.Id);
                    break;
                default:
                    apiError = ApiError.CreateGeneralException(exception);
                    _logger.ApiGeneralExceptionLogging(exception, apiError.Id);
                    break;
            }

            var result = JsonConvert.SerializeObject(apiError, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)apiError.MessageCode;
            return httpContext.Response.WriteAsync(result);
        }

    }

}
