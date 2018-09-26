using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Ksu.Gdc.Api.Core.Configurations;

namespace Ksu.Gdc.Api.Core.Models
{
    public class Dto_Game
    {
        public int GameId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public int GroupId { get; set; }

        public string Url { get; set; }

        public string ImageUrl => AppConfiguration.GetConfig("Api_Url") + "/portfolio/games/" + GameId + "/" + "thumbnail-image";
    }

    public class CreateDto_Game
    {

    }

    public class UpdateDto_Game
    {

    }
}
