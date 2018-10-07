﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ksu.Gdc.Api.Data.Entities
{
    public class ModelEntity_User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [ForeignKey("Officer")]
        public int? OfficerId { get; set; }
        public ModelEntity_Officer Officer { get; set; }
    }
}
