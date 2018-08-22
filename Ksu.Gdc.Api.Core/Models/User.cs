using System;
using System.ComponentModel.DataAnnotations;

using Ksu.Gdc.Api.Core.Contracts;

namespace Ksu.Gdc.Api.Core.Models
{
    public class UserDto : IUser
    {
        public int Id { get; set; }

        public string Username { get; set; }
    }
}
