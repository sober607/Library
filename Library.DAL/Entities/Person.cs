using System;
using System.Collections.Generic;

namespace Library.DAL.Entities
{
    public class Person : BaseEntity
    {
        public string FirstName { get; set; }
#nullable enable
        public string? LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public long? CountryId { get; set; }
#nullable disable
        public Country CountryOfBirth { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
