using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ksu.Gdc.Api.Data.Entities
{
    public class ModelEntity_Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GameId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [ForeignKey("UserId")]
        public ModelEntity_User User { get; set; }
        public int? UserId { get; set; }

        [ForeignKey("GroupId")]
        public ModelEntity_Group Group { get; set; }
        public int? GroupId { get; set; }

        [Url]
        public string Url { get; set; }
    }
}
