using System;
using System.Collections.Generic;

#nullable disable

namespace dotNet.Models
{
    public partial class Project
    {
        public Project()
        {
            Issues = new HashSet<Issue>();
        }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public int? GroupId { get; set; }
        public bool? Status { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }
    }
}
