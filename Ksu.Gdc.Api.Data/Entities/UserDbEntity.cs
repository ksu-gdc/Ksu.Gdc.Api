using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Ksu.Gdc.Api.Core.Contracts;

namespace Ksu.Gdc.Api.Data.Entities
{
    public class UserDbEntity : IUser
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Description { get; set; }
    }
}
