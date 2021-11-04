using System;
using System.Collections.Generic;

#nullable disable

namespace dotNet.Models
{
    public partial class Issue
    {
        public int IssueId { get; set; }
        public string Title { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? StartDate { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int? State { get; set; }
        public int? Creator { get; set; }
        public int? ProjectId { get; set; }

        public virtual Project Project { get; set; }
    }
}
