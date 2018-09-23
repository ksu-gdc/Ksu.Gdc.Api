using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ksu.Gdc.Api.Data.Entities
{
    public class ModelEntity_Officer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OfficerId { get; set; }

        [Required]
        public string Position { get; set; }

        [ForeignKey("UserId")]
        public ModelEntity_User User { get; set; }

        public int UserId { get; set; }
    }
}
