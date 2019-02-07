using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ksu.Gdc.Api.Data.Entities
{
    public class DbEntity_Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public byte[] Data { get; set; }

        [Required]
        [MaxLength(20)]
        public string ContentType { get; set; }

        [Required]
        public DateTimeOffset CreatedOn { get; set; }
    }
}
