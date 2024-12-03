using System.Transactions;

namespace Finance_control.Models
{
    public class Category
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Категории могут быть уникальными для каждого пользователя
        public User User { get; set; }

        public string Name { get; set; } // Название категории
        public string Type { get; set; } // "Доход" или "Расход"

        public ICollection<Transaction> Transactions { get; set; }
    }
}