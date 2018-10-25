using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ksu.Gdc.Api.Core.Models
{
    public class Dto_Group
    {
        public int GroupId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }

    public class CreateDto_Group
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Url]
        public string ImageUrl { get; set; }
    }

    public class UpdateDto_Group
    {
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Url]
        public string ImageUrl { get; set; }
    }
}
