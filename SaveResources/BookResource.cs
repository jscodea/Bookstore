using Bookstore.Entities;
using Microsoft.Build.Framework;

namespace Bookstore.SaveResources
{
    public class BookResource
    {
        public uint Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public uint? AuthorId { get; set; }
    }
}
