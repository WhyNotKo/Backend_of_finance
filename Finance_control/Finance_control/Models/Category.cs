using System.Transactions;

namespace Finance_control.Models
{
    public class Category
    {
        public required int Id { get; set; }

        public required string Name { get; set; } // Название категории

        // Связь с пользователем
        public int UserId { get; set; }
        public required User User { get; set; }

        // Навигационные свойства
        public List<Transaction>? Transactions { get; set; }
    }
}