using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ksu.Gdc.Api.Core.Models
{
    public class UpdateDto_Image
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public byte[] Data { get; set; }

        [Required]
        [MaxLength(20)]
        public string ContentType { get; set; }
    }
}
