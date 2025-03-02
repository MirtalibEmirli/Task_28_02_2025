using DAL.SqlServer.Context;
using Dapper;
using Domain.Entities;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure;

public class SqlUserRepository(string connection, TDBContext
    context) : BaseSqlRepository(connection),
    IUserRepository
{
    private readonly TDBContext _context = context;


    public   async Task<IQueryable<User>> GetAll()
    {

        return   _context.Users.AsQueryable();

    }

    public async Task<User> GetByEmailAsync(string email)
    {
        var sql = @"Select * From Users u 
                     Where u.IsDeleted =0 And u.Email=@Email";
        using var conn =OpenConnection();
        return await conn.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
    }

    public   async Task<User> GetById(int id)
    {
        var sql = @"SELECT u.* FROM USERS u WHERE  u.[Id]=@id AND u.[IsDeleted] = 0";

        using var conn = OpenConnection();  
        return  await  conn.QueryFirstOrDefaultAsync<User>(sql,new { id = id });                                
    }

    public Task Login(User user)
    {
        throw new NotImplementedException();
    }

    public async Task RegisterUser(User user)
    {
        var sql = @"INSERT INTO Users(
        Name,
        Surname,
        Email,
        Username,
        Birthdate,
        Gender,
        UserType,
        MobilePhone,
        CardNumber
        )
        VALUES(
        @Name,
        @Surname,
        @Email,
        @Username,
        @Birthdate,
        @Gender,
        @UserType,
        @MobilePhone,
        @CardNumber
        
        );
        SELECT SCOPE_IDENTITY();";
        using var conn = OpenConnection();
        var generatedID = await conn.ExecuteScalarAsync<int>(sql, user);
        user.Id = generatedID;
    }

    public Task Remove(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUser(User user)
    {
        throw new NotImplementedException();
    }
}
