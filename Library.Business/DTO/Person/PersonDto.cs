using System;
using System.Collections.Generic;

namespace Library.Business.DTO.Person
{
    public class PersonDto
    {
        public long Id { get; set; }

        public string FirstName { get; set; }
#nullable enable
        public string? LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public long? CountryId { get; set; }
#nullable disable
        public IList<long> BooksIds { get; set; }
    }
}
