
using Repository.Repositories;

namespace Repository.Common;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IBookRepository BookRepository { get; }
    Task<int> SaveChangesAsync();
}
