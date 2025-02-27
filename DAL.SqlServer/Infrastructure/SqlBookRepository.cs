using Domain.Entities;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure
{
    public class SqlBookRepository : IBookRepository
    {
        public Task AddBook(Book book)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBook(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Book>> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetBookById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
