using System.Transactions;

namespace Finance_control.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; } // Логин пользователя
        public string? Email { get; set; }
        public required string PasswordHash { get; set; } // Хранение хэша пароля
        public DateTime CreatedAt { get; set; } // Дата регистрации
        //public required ICollection<Transaction> Transactions { get; set; }
        //public required ICollection<Category> Categories { get; set; }

        // Навигационные свойства
        public required List<Transaction> Transactions { get; set; }
        public required List<Category> Categories { get; set; }

        // Бизнес-логика
        public decimal CalculateTotalBalance()
        {
            return Transactions?.Sum(t => t.IsIncome ? t.Amount : -t.Amount) ?? 0;
        }
    }
}
