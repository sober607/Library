using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.DAL.Entities
{
    public class PublishingHouse : BaseEntity
    {
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
