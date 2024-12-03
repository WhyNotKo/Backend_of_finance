namespace Finance_control.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Связь с пользователем
        public User User { get; set; }

        public int CategoryId { get; set; } // Связь с категорией
        public Category Category { get; set; }

        public decimal Amount { get; set; } // Сумма транзакции
        public DateTime Date { get; set; } // Дата транзакции
        public string Description { get; set; } // Описание
        public bool IsIncome { get; set; } // Доход или расход
    }
}
