using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ksu.Gdc.Api.Core.Models
{
    public class Dto_Group
    {
        public int GroupId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int MemberCount => Users.Count;

        public ICollection<Dto_User> Users { get; set; } = new List<Dto_User>();
    }

    public class CreateDto_Group
    {

    }

    public class UpdateDto_Group
    {

    }


}
