using PlanInv.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace PlanInv.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound, "Recurso não encontrado");
            }
            catch (UnauthorizedAccessException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.Forbidden, "Acesso negado");
            }
            catch (ArgumentException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError,
                    "Ocorreu um erro ao processar sua requisição");
            }
        }

        private static Task HandleExceptionAsync(
            HttpContext context,
            Exception exception,
            HttpStatusCode statusCode,
            string message)
        {

            if (context.Response.HasStarted)
            {
                return Task.CompletedTask;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                error = message,
                statusCode = (int)statusCode
            };


            return context.Response.WriteAsJsonAsync(response);
        }
    }
}