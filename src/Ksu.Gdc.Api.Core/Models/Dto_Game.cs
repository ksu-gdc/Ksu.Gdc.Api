using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Ksu.Gdc.Api.Core.Configurations;

namespace Ksu.Gdc.Api.Core.Models
{
    public class CreateDto_Game
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Url]
        public string HostUrl { get; set; }
    }

    public class Dto_Game
    {
        public int GameId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string HostUrl { get; set; }
    }

    public class UpdateDto_Game
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Url]
        public string HostUrl { get; set; }
    }
    public class PatchDto_Game
    {
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Url]
        public string HostUrl { get; set; }
    }
}
