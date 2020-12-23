using System;
using System.Net;
using System.Threading.Tasks;
using ApplicationServices.ApplicationServicesExceptions;
using CurrencyExchangeDomain.DomainExceptions;
using Microsoft.AspNetCore.Http;
using Ports.RepositoryExceptions;
using WebApi.Models;

namespace WebApi.ExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (DomainException ex)
            {
                await HandleDomainExceptionAsync(httpContext, ex);
            }
            catch (CurrencySymbolNotAvailableException ex)
            {
                await HandleRepositoryExceptionAsync(httpContext, ex);
            }
            catch (FixerErrorException ex)
            {
                await HandleFixerExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                await HandleUnexpectedExceptionAsync(httpContext, ex);
            }
        }
        private Task HandleDomainExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }
        
        private Task HandleRepositoryExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }
        
        private Task HandleFixerExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }
        
        private Task HandleUnexpectedExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }
    }
}