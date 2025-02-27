using Microsoft.Data.SqlClient;

namespace DAL.SqlServer.Infrastructure;

public class BaseSqlRepository
{

    private readonly string _connectionString;

	public BaseSqlRepository(string conn)
	{
			_connectionString = conn;
	}

	protected SqlConnection OpenConnection()
	{
		var conn = new SqlConnection(_connectionString);
		conn.Open();
		return conn;	
	}
}
