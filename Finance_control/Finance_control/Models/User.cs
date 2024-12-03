using System.Transactions;

namespace Finance_control.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } // Логин пользователя
        public string Email { get; set; }
        public string PasswordHash { get; set; } // Хранение хэша пароля
        public DateTime CreatedAt { get; set; } // Дата регистрации
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
