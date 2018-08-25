using System;
using System.ComponentModel.DataAnnotations;

namespace Ksu.Gdc.Api.Data.Entities
{
    public class User_Game
    {
        [Key]
        public int UserId { get; set; }

        [Key]
        public int GameId { get; set; }
    }
}
