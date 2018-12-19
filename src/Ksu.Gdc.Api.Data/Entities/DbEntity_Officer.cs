using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ksu.Gdc.Api.Data.Entities
{
    public class DbEntity_Officer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OfficerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Position { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public DbEntity_User User { get; set; }

        [Required]
        public DateTimeOffset CreatedOn { get; set; }

        [Required]
        public DateTimeOffset UpdatedOn { get; set; }
    }
}
