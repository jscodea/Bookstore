using Bookstore.Entities;
using Bookstore.SaveResources;

namespace Bookstore.DTO
{
    public class AuthorDTO
    {
        public AuthorDTO()
        {
            Books = new HashSet<BookResource>();
        }

        public uint Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public virtual IEnumerable<BookResource> Books { get; set; }
    }
}
