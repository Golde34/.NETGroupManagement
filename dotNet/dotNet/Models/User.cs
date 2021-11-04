using System;
using System.Collections.Generic;

#nullable disable

namespace dotNet.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Profileimage { get; set; }
        public bool? Status { get; set; }
    }
}
