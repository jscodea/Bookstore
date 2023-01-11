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
    public class AuthorController : ControllerBase
    {
        private readonly bookstoreContext _context;

        public AuthorController(bookstoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorResource>>> GetAuthors()
        {
            var List = await _context.Authors.Select(
                s => new AuthorResource
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            
            return List;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorResource>> GetAuthorById(int id)
        {
            AuthorResource? Author = await _context.Authors.Select(s => new AuthorResource
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
            }).FirstOrDefaultAsync(s => s.Id == id);
            if (Author == null)
            {
                return NotFound();
            }
            
            return Author;
        }

        [HttpPost]
        public async Task<IActionResult> InsertAuthor(AuthorResource Author)
        {
            var entity = new Author()
            {
                FirstName = Author.FirstName,
                LastName = Author.LastName
            };
            _context.Authors.Add(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(AuthorResource Author)
        {
            var entity = await _context.Authors.FirstOrDefaultAsync(s => s.Id == Author.Id);

            if (entity == null)
            {
                return NotFound();
            }
            entity.FirstName = Author.FirstName;
            entity.LastName = Author.LastName;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var Author = await _context.Authors.FindAsync(id);
            if (Author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(Author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("AuthorsWithBooks")]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthorsWithBooks()
        {
            var List = await _context.Authors.Include(p => p.Books).Select(
                s => new AuthorDTO
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Books = s.Books.Select(x => new BookResource {
                        Id = x.Id,
                        Title = x.Title,
                        AuthorId = x.AuthorId
                    })
                }
            ).ToListAsync();

            return List;
            
        }
    }
}
