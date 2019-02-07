using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ksu.Gdc.Api.Data.Entities
{
    public class DbEntity_UserImage
    {
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public DbEntity_User User { get; set; }

        [Required]
        [ForeignKey("Image")]
        public int ImageId { get; set; }
        public DbEntity_Image Image { get; set; }

        [Required]
        public DateTimeOffset CreatedOn { get; set; }
    }
}
