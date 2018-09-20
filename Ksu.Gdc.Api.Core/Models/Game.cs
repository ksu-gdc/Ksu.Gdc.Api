using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Ksu.Gdc.Api.Core.Configurations;

namespace Ksu.Gdc.Api.Core.Models
{
    public class GameDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string ImageUrl
        {
            get
            {
                return AppConfiguration.GetConfig("Api_Url") + "/portfolio/games/" + Id + "/" + "thumbnail-image";
            }
        }
    }
}
