using BookStore.API.Contracts;
using BookStore.Core.Models;
using BookStore.DataAccess.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers;

    [ApiController]
    [Route("controller")]
    public class BooksController
    {
        private readonly IBooksService booksService;

        public BooksController(IBooksService booksService)
        {
            this.booksService = booksService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BooksResponse>>> GetBooks()
        {
            var books = await booksService.GetAllBooks();
            var response = books
                .Select(b => new BooksResponse(b.Id, b.Title, b.Description, b.Price)).ToList();
            return new OkObjectResult(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateBook([FromBody] BooksRequest request)
        {
            var (book, error) = Book.Create(
                Guid.NewGuid(),
                request.Title,
                request.Description,
                request.Price);

            if (!string.IsNullOrWhiteSpace(error))
            {
                return new BadRequestObjectResult(error);
            }
            
            var bookId = booksService.CreateBook(book);
            return new OkObjectResult(bookId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateBook(Guid id, [FromBody] BooksRequest request)
        {
            var bookId = await booksService.UpdateBook(id, request.Title,
                request.Description, request.Price);
            return new OkObjectResult(bookId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteBook(Guid id)
        {
            return new OkObjectResult(booksService.DeleteBook(id));
        }
    }
