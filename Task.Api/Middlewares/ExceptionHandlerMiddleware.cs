using Common.Exceptions;
using Common.GlobalResponses;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace Task.Api.Middlewares;

public class ExceptionHandlerMiddleware : IMiddleware
{

    public async System.Threading.Tasks.Task InvokeAsync(HttpContext context, RequestDelegate next)
    {


        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var message = new List<string> { ex.Message };
            var statusCode = HttpStatusCode.InternalServerError;
            switch (ex)
            {
                case BadRequestException:
                    statusCode = HttpStatusCode.BadRequest; break;
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound; break;
                case ValidationException  exception:
                    statusCode = HttpStatusCode.InternalServerError;
                    await WriteValidationError(context, statusCode, exception);
                    return;

                default:
                    break;
            }

            await WriteError(context, statusCode, message);
        }

    }

    private async System.Threading.Tasks.Task WriteError(HttpContext context, HttpStatusCode statusCode, List<string> message)
    {
        context.Response.Clear();
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";
        var json = JsonSerializer.Serialize(new BaseResponseModel(message));
        await context.Response.WriteAsync(json);
    }

    private async System.Threading.Tasks.Task WriteValidationError(HttpContext context , HttpStatusCode statusCode, ValidationException ex)
    {
        context.Response.Clear();
        context .Response.StatusCode = (int)statusCode;
        context .Response.ContentType = "application/json"; 


        var validationErrors = ex.Errors.Select(e=>new {field=e.PropertyName,errorMessage=e.ErrorMessage} );
        //biz burda yen bir tip yaraddiq

        var json = JsonSerializer.Serialize(new {validationErrors=validationErrors});
        await context.Response.WriteAsync(json);    
    }

}
