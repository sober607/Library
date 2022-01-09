using System.Collections.Generic;

namespace Library.DAL.Entities
{
    public class PublishingHouse : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
