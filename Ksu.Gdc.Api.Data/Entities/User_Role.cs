using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ksu.Gdc.Api.Data.Entities
{
    public class User_Role
    {
        [Key]
        public int UserId { get; set; }

        [Key]
        public int RoleId { get; set; }
    }
}
