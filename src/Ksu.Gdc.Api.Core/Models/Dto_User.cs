using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ksu.Gdc.Api.Core.Models
{
    public class CreateDto_User
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public CreateDto_User(CASAttributes attributes)
        {
            UserId = attributes.KsuPersonWildcatId[0];
            Username = attributes.Uid[0];
        }
    }

    public class Dto_User
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Description { get; set; }

        public string Email => Username + "@ksu.edu";
    }
    public class AuthDto_User : Dto_User
    {
        public bool IsOfficer { get; set; }
    }

    public class UpdateDto_User
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
    public class PatchDto_User
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}
