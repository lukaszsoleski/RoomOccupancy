using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RoomOccupancy.Application.Exceptions;
using RoomOccupancy.Common.Extentions;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RoomOccupancy.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.BadRequest;
            var errorDto = new ErrorResponce();

            if (exception is ValidationException)
            {
                HandleValidationException(exception, errorDto);
            }
            else
            {
                HandleApplicationException(exception, errorDto);
            }

            statusCode = GetResponseStatus(exception, statusCode);

            await WriteResponce(errorDto, context, statusCode);
        }

        private static HttpStatusCode GetResponseStatus(Exception exception, HttpStatusCode statusCode)
        {
            var exType = exception.GetType();

            if (!exType.FullName.Contains("Room"))
            {
                statusCode = HttpStatusCode.InternalServerError;
            }
            else if (exception is NotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
            }

            return statusCode;
        }

        private static void HandleValidationException(Exception exception, ErrorResponce errorDto)
        {
            {
                var ex = (ValidationException)exception;

                errorDto.Message = ex.Message;
                errorDto.Errors = ex.Failures.Select(x => x.Value)
                    .SelectMany(x => x)
                    .Distinct()
                    .ToList();
            }
        }

        private static void HandleApplicationException(Exception exception, ErrorResponce errorDto)
        {
            var failures = exception.WithFailures().ToList();

            if (!failures.Any())
                return;

            errorDto.Message = failures.FirstOrDefault();
            failures.RemoveAt(0);
            errorDto.Errors = failures;
        }

        private async Task WriteResponce(ErrorResponce responseObj, HttpContext context, HttpStatusCode statusCode)
        {
            var response = context.Response;

            var content = JsonConvert.SerializeObject(responseObj);

            response.ContentType = "application/json";

            response.StatusCode = (int)statusCode;

            await response.WriteAsync(content);
        }
    }
}