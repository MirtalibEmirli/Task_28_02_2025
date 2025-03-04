using Dapper;
using Domain.Entities;
using Repository.Common;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure;


public class SqlImageRepository(string conn) : BaseSqlRepository(conn), IImageRepository
{
    public async Task<int> AddImage(Image image)
    {
        var sql = @"INSERT INTO Images (FileName, Location, CreatedAt) 
                    VALUES (@FileName, @Location, @CreatedAt);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

        var _connection = OpenConnection();
        return await _connection.ExecuteScalarAsync<int>(sql, image);
    }

    public Task DeleteImage(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<Image>> GetAllImages()
    {
        throw new NotImplementedException();
    }

    public Task<Image> GetImageById(int id)
    {
        throw new NotImplementedException();

    }

    public Task UpdateImage(Image image)
    {
        throw new NotImplementedException();
    }
}
