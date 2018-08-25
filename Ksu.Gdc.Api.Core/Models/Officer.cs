using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Ksu.Gdc.Api.Core.Configurations;

namespace Ksu.Gdc.Api.Core.Models
{
    public class OfficerDto
    {
        public string Position { get; set; }

        public UserDto User { get; set; }

        public string ImageUrl
        {
            get
            {
                var baseUrl = OfficerConfig.ImageDataStoreUrl;
                var title = User.Id.ToString();
                return baseUrl + "/" + title + ".jpg";
            }
        }
    }

    public class OfficerForCreationDto
    {
        [Required]
        public string Position { get; set; }

        [Required]
        public UserForCreationDto User { get; set; }
    }
}