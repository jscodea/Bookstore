using Bookstore.DTO;
using System;
using System.Collections.Generic;
using static System.Reflection.Metadata.BlobBuilder;

namespace Bookstore.Entities
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public uint Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
