﻿namespace Finance_control.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public required string Role { get; set; } // "Admin" или "User"

        public bool IsAdmin() => Role.Equals("Admin", StringComparison.OrdinalIgnoreCase);
    }
}
