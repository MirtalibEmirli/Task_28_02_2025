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

    public IUserRepository UserRepository => SqlUserRepository ?? new SqlUserRepository(connection, _context);
    public SqlUserRepository? SqlUserRepository;


    public SqlImageRepository SqlImageRepository;
    public IBookRepository BookRepository => SqlBookRepository ??new SqlBookRepository(connection, _context);

    public IImageRepository ImageRepository => SqlImageRepository ??new SqlImageRepository(_connectionString);

    public SqlBookRepository? SqlBookRepository;

    public async Task<int> SaveChangesAsync()
    {
       return  await _context.SaveChangesAsync();                  
    }
}
