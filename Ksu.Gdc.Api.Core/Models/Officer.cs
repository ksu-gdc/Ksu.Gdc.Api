using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Ksu.Gdc.Api.Core.Configurations;

namespace Ksu.Gdc.Api.Core.Models
{
    public class OfficerDto
    {
        public int Id { get; set; }

        public string Position { get; set; }

        public UserDto User { get; set; }
    }

    public class OfficerForCreationDto
    {
        [Required]
        public string Position { get; set; }

        [Required]
        public UserForCreationDto User { get; set; }
    }
}