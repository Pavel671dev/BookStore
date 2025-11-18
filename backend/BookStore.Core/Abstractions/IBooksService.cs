using BookStore.Core.Models;

namespace BookStore.DataAccess.Repositories;

public interface IBooksService
{
    Task<Guid> CreateBook(Book book);
    Task<Guid> UpdateBook(Guid id, string title,
        string description, decimal price);
    Task<Guid> DeleteBook(Guid id);
    Task<List<Book>> GetAllBooks();
}