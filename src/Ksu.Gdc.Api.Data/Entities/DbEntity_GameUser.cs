using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ksu.Gdc.Api.Data.Entities
{
    public class DbEntity_GameUser
    {
        [Required]
        [ForeignKey("Game")]
        public int GameId { get; set; }
        public DbEntity_Game Game { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public DbEntity_User User { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
    }
}
