using System;
using System.ComponentModel.DataAnnotations;

namespace Ksu.Gdc.Api.Data.Entities
{
    public class Group_User
    {
        [Key]
        public int UserId { get; set; }

        [Key]
        public int GroupId { get; set; }
    }
}
