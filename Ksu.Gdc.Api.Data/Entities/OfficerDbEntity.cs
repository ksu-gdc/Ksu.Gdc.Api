using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Ksu.Gdc.Api.Core.Contracts;

namespace Ksu.Gdc.Api.Data.Entities
{
    public class OfficerDbEntity : IOfficer<UserDbEntity>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Position { get; set; }

        [ForeignKey("UserId")]
        public UserDbEntity User { get; set; }

        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Description { get; set; }
    }
}
