﻿using System.ComponentModel.DataAnnotations;

namespace Library.DAL.Entities
{
    public class BaseEntity
    {
        [Required]
        public long Id { get; set; }
    }
}
