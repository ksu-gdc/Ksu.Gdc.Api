using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Ksu.Gdc.Api.Core.Configurations;

namespace Ksu.Gdc.Api.Core.Models
{
    public class Dto_User
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Description { get; set; }

        public string ImageUrl => AppConfiguration.GetConfig("Api_Url") + "/users/" + UserId + "/profile-image";

        public string Email => Username + "@ksu.edu";
    }

    public class CreateDto_User
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        public CreateDto_User(CASAttributes attributes)
        {
            UserId = attributes.KsuPersonWildcatId[0];
            Username = attributes.Uid[0];
        }
    }

    public class UpdateDto_User
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
