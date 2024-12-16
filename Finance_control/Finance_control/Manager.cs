using Finance_control.Data;
using Finance_control.Models;
using Microsoft.EntityFrameworkCore;

namespace Finance_control
{
    public class Manager
    {
        private readonly Finance_controlContext _context;

        public Manager(Finance_controlContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transaction.ToListAsync();
        }

        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            return await _context.Transaction.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            _context.Transaction.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            _context.Entry(transaction).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction != null)
            {
                _context.Transaction.Remove(transaction);
                await _context.SaveChangesAsync();
            }
        }

        private IQueryable<Transaction> FilterTransactionsByDates(IQueryable<Transaction> query, DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue)
            {
                query = query.Where(t => t.Date >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(t => t.Date <= endDate.Value);
            }

            return query;
        }

        private async Task<float> SumTransactionsByType(IQueryable<Transaction> query, string type)
        {
            return await query
                .Where(t => t.Type == type)
                .SumAsync(t => t.Amount);
        }
        public async Task<Report> GenerateReportAsync(DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Transaction.AsQueryable();
            query = FilterTransactionsByDates(query, startDate, endDate);

            var totalIncome = await SumTransactionsByType(query, "Income");
            var totalExpense = await SumTransactionsByType(query, "Expense");

            var report = new Report
            {
                Period = DateTime.Now,
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
            };

            return report;
        }

        public async Task<float> CalculateNetBalanceAsync(DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Transaction.AsQueryable();
            query = FilterTransactionsByDates(query, startDate, endDate);

            var totalIncome = await SumTransactionsByType(query, "Income");
            var totalExpense = await SumTransactionsByType(query, "Expense");

            var report = new Report
            {
                Period = DateTime.Now,
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
            };

            return report.NetBalance();
        }

        public async Task<List<Transaction>> GetTransactionsAsyncPerDate(DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Transaction.AsQueryable();
            query = FilterTransactionsByDates(query, startDate, endDate);
            return await query.ToListAsync();
        }
    }
}
