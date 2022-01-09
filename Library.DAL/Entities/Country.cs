using System.Collections.Generic;

namespace Library.DAL.Entities
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Person> People { get; set; }
    }
}
