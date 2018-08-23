using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

using Ksu.Gdc.Api.Core.Contracts;

namespace Ksu.Gdc.Api.Core.Models
{
    public class OfficerDto : IOfficer<UserDto>
    {
        public string Position { get; set; }

        public UserDto User { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + MiddleName + " " + LastName;
            }
        }

        public string ImageUrl
        {
            get
            {
                return Environment.GetEnvironmentVariable("OfficerImageDataStoreUrl") + "/" + Regex.Replace(User.Username, "[^a-zA-Z0-9]", "");
            }
        }

        public string Description { get; set; }
    }
}