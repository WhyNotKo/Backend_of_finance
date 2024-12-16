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
    }
}
