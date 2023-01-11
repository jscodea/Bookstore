using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Entities
{
    public partial class Book
    {
        public uint Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public uint? AuthorId { get; set; }

        public virtual Author? Author { get; set; }
    }
}
