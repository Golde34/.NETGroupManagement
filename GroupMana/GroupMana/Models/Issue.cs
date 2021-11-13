namespace GroupMana.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Issue
    {
        public int issueId { get; set; }

        [Required]
        [StringLength(255)]
        public string title { get; set; }

        public DateTime? dueDate { get; set; }

        public DateTime? startDate { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        [StringLength(255)]
        public string content { get; set; }

        public int? state { get; set; }

        public int? creator { get; set; }
        public int? assignee { get; set; }

        public int? projectId { get; set; }

        public bool? status { get; set; }

        public virtual Project Project { get; set; }
    }
}
