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
                return UserConfig.DataStoreUrl + "/" + Id + "/" + "profile.jpg";
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
    }
}
