using Domain.Entities;

namespace Repository.Repositories;

public interface IBookRepository
{

    public Task AddBook(Book book); 
    public Task UpdateBook(Book book);  
    public Task DeleteBook(int id); 
    public Task<Book> GetBookById(int id);
    public Task<IQueryable<Book>> GetAllBooks();                            


}
