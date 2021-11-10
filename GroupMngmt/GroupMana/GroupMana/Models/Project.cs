namespace GroupMana.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Project
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Project()
        {
            Issues = new HashSet<Issue>();
        }

        public int projectId { get; set; }

        [StringLength(255)]
        public string projectName { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        public int? groupId { get; set; }

        public bool? status { get; set; }

        public virtual Group Group { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Issue> Issues { get; set; }
    }
}
