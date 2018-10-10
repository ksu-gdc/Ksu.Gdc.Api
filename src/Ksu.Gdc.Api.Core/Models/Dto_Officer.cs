using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Ksu.Gdc.Api.Core.Configurations;

namespace Ksu.Gdc.Api.Core.Models
{
    public class Dto_Officer
    {
        public int OfficerId { get; set; }

        public string Position { get; set; }

        public Dto_User User { get; set; }
    }

    public class CreateDto_Officer
    {
        [Required]
        [MaxLength(100)]
        public string Position { get; set; }
    }

    public class UpdateDto_Officer
    {
        [Required]
        [MaxLength(100)]
        public string Position { get; set; }

        public int UserId { get; set; }
    }
}