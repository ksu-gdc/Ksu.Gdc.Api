using System;
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

        [Required]
        [Url]
        public string HostUrl { get; set; }

        [Required]
        public DateTimeOffset CreatedOn { get; set; }

        [Required]
        public DateTimeOffset UpdatedOn { get; set; }
    }
}
