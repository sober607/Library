using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.DAL.Entities
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Person> People { get; set; }
    }
}
