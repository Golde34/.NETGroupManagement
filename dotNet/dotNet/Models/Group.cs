using System;
using System.Collections.Generic;

#nullable disable

namespace dotNet.Models
{
    public partial class Group
    {
        public Group()
        {
            Projects = new HashSet<Project>();
        }

        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
