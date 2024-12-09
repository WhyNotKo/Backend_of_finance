using System.ComponentModel.DataAnnotations;

namespace Finance_control.Models
{
    public class RegisterDto
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "User"; // Роль по умолчанию
    }
}
