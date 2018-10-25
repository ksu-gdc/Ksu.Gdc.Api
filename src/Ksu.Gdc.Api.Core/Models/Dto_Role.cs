using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ksu.Gdc.Api.Core.Models
{
    public class Dto_Role
    {
        public int RoleId { get; set; }

        public string Name { get; set; }
    }

    public class CreateDto_Role
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }

    public class UpdateDto_Role
    {
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
