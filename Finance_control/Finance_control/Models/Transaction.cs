namespace Finance_control.Models
{
    public class Transaction
    {
        private bool isIncome;

        public required int Id { get; set; }
        public required int UserId { get; set; } // Связь с пользователем
        public required int CategoryId { get; set; } // Связь с категорией
        public required decimal Amount { get; set; } // Сумма транзакции
        public required DateTime Date { get; set; } // Дата транзакции
        public string? Description { get; set; } // Описание
        public bool IsIncome { get => isIncome; set => isIncome = value; } // Доход или расход

        // Бизнес-логика
        public string GetTransactionSummary()
        {
            return $"{Date.ToShortDateString()}: {Description} ({(IsIncome ? "+" : "-")}{Amount:C})";
        }
    }
}
