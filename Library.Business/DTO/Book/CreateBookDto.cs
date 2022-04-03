using System;
using System.Collections.Generic;

namespace Library.Business.DTO.Book
{
    public class CreateBookDto
    {
        public string Title { get; set; }

        public long? PublishingHouseId { get; set; }

        public DateTime? PublishingDate { get; set; }

        public int? Circulations { get; set; }

        public IList<long> PersonsIdsToBeAuthors { get; set; }
    }
}
