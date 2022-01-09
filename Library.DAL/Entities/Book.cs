using System;
using System.Collections.Generic;

namespace Library.DAL.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }

        public ICollection<Person> Authors { get; set; }

        public long? PublishingHouseId { get; set; }

        public PublishingHouse PublishingHouse { get; set; }

        public DateTime? PublishingDate { get; set; }

        public int? Circulations { get; set; }
    }
}
