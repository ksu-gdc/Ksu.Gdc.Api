using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ksu.Gdc.Api.Data.Entities
{
    public class DbEntity_User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public bool HasVerifiedInfo { get; set; }
    }
}
