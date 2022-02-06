using System.ComponentModel.DataAnnotations;

namespace Library.DAL.Entities
{
    public class BaseEntity
    {
        [Required]
        public int Id { get; set; }
    }
}
