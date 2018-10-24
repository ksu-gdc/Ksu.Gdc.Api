using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ksu.Gdc.Api.Data.Entities
{
    public class JoinEntity_UserGroup
    {
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public ModelEntity_User User { get; set; }

        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public ModelEntity_Group Group { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    }
}
