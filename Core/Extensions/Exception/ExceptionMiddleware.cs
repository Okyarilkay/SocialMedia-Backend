using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions.Exception
{
    public class ExceptionMiddleware
    {
        RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception e)
            {

                await HandleExceptionAsync(context,e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, System.Exception e)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message = "Internal Server Error";

            if (e.GetType() == typeof(ValidationException))
            {
                message = "Bad Request";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                IEnumerable<ValidationFailure> validationErrors = ((ValidationException)e).Errors;
                return context.Response.WriteAsync(new ValidationErrorDetails()
                {
                    StatusCode = 400,
                    Message = message,
                    ValidationErrors = validationErrors
                }.ToString());
            }

            if (e.GetType() == typeof(UnauthorizedAccessException))
            {
                message = "Unauthorized.";
                context.Response.StatusCode= (int)HttpStatusCode.Unauthorized;
                return context.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = message,
                }.ToString());
            }
            return context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = message,
            }.ToString());
        }
    }
}
