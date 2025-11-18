using BookStore.Core.Models;
using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repositories;

public class BooksRepository : IBooksRepository
{
    private readonly BookStoreDbContext context;

    public BooksRepository(BookStoreDbContext context)
    {
        this.context = context;
    }

    public async Task<List<Book>> Get()
    {
        var bookEntities = await context.Books
            .AsNoTracking()
            .ToListAsync();
        
        var books = bookEntities
            .Select(b => Book.Create(b.Id, b.Title, b.Description, b.Price).Book).ToList();
        return books;
    }

    public async Task<Guid> Create(Book book)
    {
        var BookEntity = new BookEntity
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            Price = book.Price
        };
        await context.Books.AddAsync(BookEntity);
        await context.SaveChangesAsync();

        return BookEntity.Id;
    }

    public async Task<Guid> Update(Guid id,
        string title, string description, decimal price)
    {
        await context.Books
            .Where(b => b.Id == id)
            .ExecuteUpdateAsync(b => b
                .SetProperty(s => s.Title, s => title)
                .SetProperty(s => s.Description, s => title)
                .SetProperty(s => s.Price, s => price));
        return id;
    }

    public async Task<Guid> Delete(Guid id)
    {
        await context.Books
            .Where(b => b.Id == id)
            .ExecuteDeleteAsync();
        return id;
    }
}