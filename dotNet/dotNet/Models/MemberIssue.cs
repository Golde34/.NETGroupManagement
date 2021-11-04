using System;
using System.Collections.Generic;

#nullable disable

namespace dotNet.Models
{
    public partial class MemberIssue
    {
        public int? UserId { get; set; }
        public int? IssueId { get; set; }

        public virtual Issue Issue { get; set; }
        public virtual User User { get; set; }
    }
}
