using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ksu.Gdc.Api.Data.Entities
{
    public class UserDbEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Description { get; set; }
    }
}
