namespace Finance_control.Models
{
    public class Report
    {
        public DateTime Period { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }

        public decimal NetBalance() => TotalIncome - TotalExpense;
    }
}
