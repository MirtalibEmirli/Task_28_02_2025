using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Task.Api.Services;

public static class AuthenticationService
{

    public static IServiceCollection AddAuthenticationService(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddAuthentication(opt =>
        {
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme =JwtBearerDefaults.AuthenticationScheme;


        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateAudience = true,//bu hansi saytlardan gelecek request onu deyir
                ValidateIssuer = true,    //yaradilacaq tokenin kimin verececyini demekdir yeni api dir bu deyqe bizde bu 
                ValidateIssuerSigningKey = true,//yaradilacaq tokenin app e aid bir deyer olacaqani ifade eden bir secure key 
                ValidateLifetime = true, //tokenin suresini kontrol eden dogrulama
                ValidAudience = configuration["JWT:ValidAuidience"],
                ValidIssuer = configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!))
            };
        });
        return services;    
    }
}
