using DAL.SqlServer;
using Application;
using Task.Api.Middlewares;
using Task.Api.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conn = builder.Configuration.GetConnectionString("Default");
builder.Services.AddSqlServerServices(conn);
builder.Services.AddTransient<ExceptionHandlerMiddleware>();
builder.Services.AddApplicationServices();
builder.Services.AddAuthenticationService(builder.Configuration);

var app = builder.Build();



// 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseStaticFiles();
app.MapControllers();

app.Run();
