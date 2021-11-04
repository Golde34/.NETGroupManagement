using System;
using System.Collections.Generic;

#nullable disable

namespace dotNet.Models
{
    public partial class Menber
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public int? RoleId { get; set; }
        public bool? Status { get; set; }

        public virtual Group Group { get; set; }
        public virtual Role Role { get; set; }
    }
}
