using System.ComponentModel.DataAnnotations;

namespace Library.DAL.Entities
{
    public class BookAuthor : BaseEntity
    {
        public long BookId { get; set; }

#nullable enable
        public Book? Book { get; set; }

        public long AuthorId { get; set; }

        public Person? Author { get; set; }
#nullable disable
    }
}
