
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace DAL.SqlServer.Context;

public class TDBContext: DbContext
{
    public TDBContext(DbContextOptions<TDBContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }  
}
