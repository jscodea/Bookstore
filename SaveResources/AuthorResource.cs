using Bookstore.Entities;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.SaveResources
{
    public class AuthorResource
    {
        public uint Id { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
    }
}
