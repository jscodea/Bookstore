using Bookstore.Entities;
using Bookstore.SaveResources;
using Microsoft.Build.Framework;

namespace Bookstore.DTO
{
    public class BookDTO
    {
        public uint Id { get; set; }
        public string? Title { get; set; }
        public AuthorResource? Author { get; set; }
    }
}
