namespace GroupMngmt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public int userID { get; set; }

        [Required]
        [StringLength(255)]
        public string username { get; set; }

        [Required]
        [StringLength(255)]
        public string password { get; set; }

        [Required]
        [StringLength(255)]
        public string fullname { get; set; }

        [StringLength(255)]
        public string email { get; set; }

        [StringLength(255)]
        public string profileimage { get; set; }

        public bool? status { get; set; }
    }
}
