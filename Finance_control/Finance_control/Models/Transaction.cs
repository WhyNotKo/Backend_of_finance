namespace Finance_control.Models
{
    public class Transaction
    {   
        public int Id { get; set; }
        public required DateTime Date { get; set; }
        public required string Type { get; set; } // "Income" или "Expense"
        public required int Amount { get; set; }
        public string? Category { get; set; }

        public bool IsIncome() => Type.Equals("Income", StringComparison.OrdinalIgnoreCase);
        public bool IsExpense() => Type.Equals("Expense", StringComparison.OrdinalIgnoreCase);
    }
}
