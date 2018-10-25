using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ksu.Gdc.Api.Data.Entities
{
    public class JoinEntity_UserGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserGroupId { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public ModelEntity_User User { get; set; }

        [Required]
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public ModelEntity_Group Group { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    }
}
