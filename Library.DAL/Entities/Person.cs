using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.DAL.Entities
{
    public class Person : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
#nullable enable
        public string? LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public long? CountryId { get; set; }
#nullable disable
        public Country CountryOfBirth { get; set; }

        public IList<BookAuthor> AuthorBooks { get; set; }
    }
}
