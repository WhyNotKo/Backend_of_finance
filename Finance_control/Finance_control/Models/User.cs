﻿using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Finance_control.Models
{
    public class User
    {
        [Key]
        public required string Login { get; set; }
        private byte[] password;
        public string Password
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var b in MD5.Create().ComputeHash(password))
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
            set { password = Encoding.UTF8.GetBytes(value); }
        }
        public bool IsAdmin => Login == "admin";

        public bool CheckPassword(string password) => password == Password;
    }
}
