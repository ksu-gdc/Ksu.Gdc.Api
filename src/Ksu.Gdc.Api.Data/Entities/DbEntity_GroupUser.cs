using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ksu.Gdc.Api.Data.Entities
{
    // Key: { GroupId, UserId }
    public class DbEntity_GroupUser
    {
        [Required]
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public DbEntity_Group Group { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public DbEntity_User User { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset UpdatedOn { get; set; }
    }
}
