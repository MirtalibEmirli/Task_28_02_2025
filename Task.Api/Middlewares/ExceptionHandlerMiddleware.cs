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
                case ValidationException exception:
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

    private async System.Threading.Tasks.Task WriteValidationError(HttpContext context, HttpStatusCode statusCode, ValidationException ex)
    {
        context.Response.Clear();
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";


        var validationErrors = ex.Errors.Select(e => new { field = e.PropertyName, errorMessage = e.ErrorMessage });
        //biz burda yen bir tip yaraddiq

        var json = JsonSerializer.Serialize(new { validationErrors = validationErrors });
        await context.Response.WriteAsync(json);
    }

}
//using Domain.Entities;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Cryptography;
//using System.Text;

//public class TokenHandler
//{
//    public IConfiguration Configuration { get; set; }
//    public TokenHandler(IConfiguration configuration)
//    {
//        Configuration = configuration;
//    }
//    //Token üretecek metot.
//    public TokenAuthentication.Models.Token CreateAccessToken(User user)
//    {
//        Models.Token tokenInstance = new Models.Token();

//        //Security  Key'in simetriğini alıyoruz.
//        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));

//        //Şifrelenmiş kimliği oluşturuyoruz.
//        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//        //Oluşturulacak token ayarlarını veriyoruz.
//        tokenInstance.Expiration = DateTime.Now.AddMinutes(5);
//        JwtSecurityToken securityToken = new JwtSecurityToken(
//            issuer: Configuration["Token:Issuer"],
//            audience: Configuration["Token:Audience"],

//            expires: tokenInstance.Expiration,//Token süresini 5 dk olarak belirliyorum
//            notBefore: DateTime.Now,//Token üretildikten ne kadar süre sonra devreye girsin ayarlıyouz.
//            signingCredentials: signingCredentials
//            );
//  //      "JWT": {
//  //          "ValidAudience": "https://localhost:7182",
//  //  "ValidIssuer": "https://localhost:7182",
//  //  "Secret": "dwicyr8n4_3- 567 &^^%$@$%(873564756347tgyeb",
//  //  "AccessTokenExpirationMinutes": 30,
//  //  "RefreshTokenExpirationDays": 7
//  //},

//        //Token oluşturucu sınıfında bir örnek alıyoruz.
//        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

//        //Token üretiyoruz.
//        tokenInstance.AccessToken = tokenHandler.WriteToken(securityToken);

//        //Refresh Token üretiyoruz.
//        tokenInstance.RefreshToken = CreateRefreshToken();
//        return tokenInstance;
//    }

//    //Refresh Token üretecek metot.
//    public string CreateRefreshToken()
//    {
//        byte[] number = new byte[32];
//        using (RandomNumberGenerator random = RandomNumberGenerator.Create())
//        {
//            random.GetBytes(number);
//            return Convert.ToBase64String(number);
//        }
//    }
//}