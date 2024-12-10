namespace Finance_control.Models
{
    public class Report
    {
        public DateTime Period { get; set; }
        public float TotalIncome { get; set; }
        public float TotalExpense { get; set; }

        public float NetBalance() => TotalIncome - TotalExpense;
    }
}
