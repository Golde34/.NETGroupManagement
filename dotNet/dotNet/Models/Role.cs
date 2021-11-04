using System;
using System.Collections.Generic;

#nullable disable

namespace dotNet.Models
{
    public partial class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool? Status { get; set; }
    }
}
