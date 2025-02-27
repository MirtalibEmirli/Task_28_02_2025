namespace DAL.SqlServer.UnitOfWork;

using DAL.SqlServer.Context;
using DAL.SqlServer.Infrastructure;
using Repository.Common;
using Repository.Repositories;
using System;
using System.Threading.Tasks;

public class SqlUnitOfWork(string connection, TDBContext context) : IUnitOfWork
{
    private readonly string _connectionString= connection;
    private readonly TDBContext _context = context;

    public SqlUserRepository SqlUserRepository;
    public SqlBookRepository SqlBookRepository;
    public IUserRepository UserRepository => throw new NotImplementedException();

    public IBookRepository BookRepository => throw new NotImplementedException();

    public async Task<int> SaveChangesAsync()
    {
       return  await _context.SaveChangesAsync();                  
    }
}
