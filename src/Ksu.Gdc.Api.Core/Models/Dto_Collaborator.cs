using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ksu.Gdc.Api.Core.Models
{
    public class CreateDto_Collaborator
    {
        [Required]
        public int UserId { get; set; }
    }

    public class Dto_Collaborator
    {
        public int UserId { get; set; }
    }

    public class UpdateDto_Collaborator
    {
    }
    public class PatchDto_Collaborator
    {
    }
}
