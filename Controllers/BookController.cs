using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bookstore.DTO;
using Bookstore.Entities;
using System.Net;
using Bookstore.SaveResources;

namespace Bookstore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly bookstoreContext _context;

        public BookController(bookstoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookResource>>> GetBooks()
        {
            var List = await _context.Books.Select(
                s => new BookResource
                {
                    Id = s.Id,
                    Title = s.Title,
                    AuthorId = s.AuthorId
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }

            return List;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookResource>> GetBookById(int id)
        {
            BookResource? Book = await _context.Books.Select(s => new BookResource
            {
                Id = s.Id,
                Title = s.Title
            }).FirstOrDefaultAsync(s => s.Id == id);
            if (Book == null)
            {
                return NotFound();
            }

            return Book;
        }

        [HttpPost]
        public async Task<IActionResult> InsertBook(BookResource Book)
        {
            var entity = new Book()
            {
                Title = Book.Title,
                AuthorId = Book.AuthorId,
            };
            _context.Books.Add(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(BookResource Book)
        {
            var entity = await _context.Books.FirstOrDefaultAsync(s => s.Id == Book.Id);

            if (entity == null)
            {
                return NotFound();
            }
            entity.Title = Book.Title;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var Book = await _context.Books.FindAsync(id);
            if (Book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(Book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("BooksWithAuthor")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooksWithAuthor()
        {
            var List = await _context.Books.Include(p => p.Author).Select(
                s => new BookDTO
                {
                    Id = s.Id,
                    Title = s.Title,
                    Author = new AuthorResource
                    {
                        Id = s.Author.Id,
                        FirstName = s.Author.FirstName,
                        LastName = s.Author.LastName,
                    }
                }
            ).ToListAsync();

            return List;
        }
    }
}
