using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ksu.Gdc.Api.Data.Entities
{
    public class DbEntity_Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GameId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public DbEntity_User User { get; set; }

        [ForeignKey("Group")]
        public int? GroupId { get; set; }
        public DbEntity_Group Group { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        [Url]
        public string ItemUrl { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;
    }
}
