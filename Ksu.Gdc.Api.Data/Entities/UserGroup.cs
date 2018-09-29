using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ksu.Gdc.Api.Data.Entities
{
    public class JoinEntity_UserGroup
    {
        [ForeignKey("UserId")]
        public ModelEntity_User User { get; set; }
        public int UserId { get; set; }

        [ForeignKey("GroupId")]
        public ModelEntity_Group Group { get; set; }
        public int GroupId { get; set; }
    }
}
