using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ksu.Gdc.Api.Data.Entities
{
    public class DbEntity_GameImage
    {
        [Required]
        [ForeignKey("Game")]
        public int GameId { get; set; }
        public DbEntity_Game Game { get; set; }

        [Required]
        [ForeignKey("Image")]
        public int ImageId { get; set; }
        public DbEntity_Image Image { get; set; }

        [Required]
        public DateTimeOffset CreatedOn { get; set; }
    }
}
