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

        [Required]
        public bool HasVerifiedInfo { get; set; }

        public CreateDto_User(CASAttributes attributes)
        {
            UserId = attributes.KsuPersonWildcatId;
            Username = attributes.Uid;
        }
    }

    public class AuthDto_User
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public bool HasVerifiedInfo { get; set; }
    }

    public class Dto_User
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public bool HasVerifiedInfo { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Description { get; set; }
    }

    public class UpdateDto_User
    {
        [Required]
        public bool HasVerifiedInfo { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }

    public class PatchDto_User
    {
        public bool HasVerifiedInfo { get; set; }

        [MinLength(1)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MinLength(1)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}
