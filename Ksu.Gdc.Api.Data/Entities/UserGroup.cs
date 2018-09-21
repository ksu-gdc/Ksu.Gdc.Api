using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ksu.Gdc.Api.Data.Entities
{
    public class JoinEntity_UserGroup
    {
        public int UserId { get; set; }

        public int GroupId { get; set; }
    }
}
