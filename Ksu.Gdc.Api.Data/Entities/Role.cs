using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ksu.Gdc.Api.Data.Entities
{
    public class ModelEntity_Role
    {
        [Key]
        public int OfficerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
