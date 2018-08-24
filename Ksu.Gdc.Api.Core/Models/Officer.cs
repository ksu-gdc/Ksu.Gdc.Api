using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Configuration;

using Ksu.Gdc.Api.Core;
using Ksu.Gdc.Api.Core.Contracts;

namespace Ksu.Gdc.Api.Core.Models
{

    public class OfficerDto : IOfficer<UserDto>
    {
        public string Position { get; set; }

        public UserDto User { get; set; }

        public string ImageUrl
        {
            get
            {
                var baseUrl = AppConfiguration.GetConfig("officerImageDataStoreUrl");
                var title = User.FullName.ToLower();
                title = Regex.Replace(title, "[ ]", "-");
                title = Regex.Replace(title, "[^a-zA-Z0-9-]", "");
                return baseUrl + "/" + title + ".png";
            }
        }
    }

    public class OfficerForCreationDto : IOfficer<UserForCreationDto>
    {
        [Required]
        public string Position { get; set; }

        [Required]
        public UserForCreationDto User { get; set; }
    }
}