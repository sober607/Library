using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Entities
{
    public class BookAuthor : BaseEntity
    {
        [Required]
        public long BookId { get; set; }

#nullable enable
        public Book? Book { get; set; }

        [Required]
        public long AuthorId { get; set; }

        public Person? Author { get; set; }
#nullable disable
    }
}
