using DAL.SqlServer.Context;
using DAL.SqlServer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.Common;

namespace DAL.SqlServer;

public static class DependencyInjection
{

    public static IServiceCollection AddSqlServerServices(this IServiceCollection services,string conn)
    {
        services.AddDbContext<TDBContext>(opt=>opt.UseSqlServer(conn));
        services.AddScoped<IUnitOfWork, SqlUnitOfWork>(opt =>
        {
            var dbContext = opt.GetRequiredService<TDBContext>();           
            return new SqlUnitOfWork(conn, dbContext);
        } );

        return services;
    }
}
