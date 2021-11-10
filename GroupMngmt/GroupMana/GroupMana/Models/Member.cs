namespace GroupMana.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Member
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int userID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int groupId { get; set; }

        public int? roleId { get; set; }

        public int? status { get; set; }

        public virtual Group Group { get; set; }

        public virtual Role Role { get; set; }

        public virtual User User { get; set; }
    }
}
