using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.DAL.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }

        public long? PublishingHouseId { get; set; }

        public PublishingHouse PublishingHouse { get; set; }

        public DateTime? PublishingDate { get; set; }

        public int? Circulations { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
