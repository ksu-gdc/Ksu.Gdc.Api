using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Ksu.Gdc.Api.Core.Configurations;

namespace Ksu.Gdc.Api.Core.Models
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Description { get; set; }

        public string ImageUrl
        {
            get
            {
                return AppConfiguration.GetConfig("Api_Url") + "/users/" + Id + "/profile-image";
            }
        }

        public string Email
        {
            get
            {
                return Username + "@ksu.edu";
            }
        }
    }

    public class UserForCreationDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        public UserForCreationDto(CASAttributes attributes)
        {
            Id = attributes.KsuPersonWildcatId[0];
            Username = attributes.Uid[0];
        }
    }

    public class UserForUpdateDto
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Description { get; set; }
    }
}
