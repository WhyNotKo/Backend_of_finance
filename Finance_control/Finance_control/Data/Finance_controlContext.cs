using Microsoft.EntityFrameworkCore;

namespace Finance_control.Data
{
    public class Finance_controlContext : DbContext
    {
        public Finance_controlContext (DbContextOptions<Finance_controlContext> options)
            : base(options)
        {
        }

        public DbSet<Finance_control.Models.Transaction> Transaction { get; set; } = default!;
        public DbSet<Finance_control.Models.User> User { get; set; } = default!;
    }
}
