using Microsoft.EntityFrameworkCore;
using HandleOverdueLoansFunction.Domain.Entities;
using HandleOverdueLoansFunction.Domain.Entities.Utils;

namespace HandleOverdueLoansFunction.Infrastructure.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext() { }

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanBook> LoanBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração do relacionamento LoanBook
            modelBuilder.Entity<LoanBook>()
                .HasKey(lb => new { lb.LoanId, lb.BookId });

            modelBuilder.Entity<LoanBook>()
                .HasOne(lb => lb.Loan)
                .WithMany(l => l.LoanBooks)
                .HasForeignKey(lb => lb.LoanId);

            modelBuilder.Entity<LoanBook>()
                .HasOne(lb => lb.Book)
                .WithMany(b => b.LoanBooks)
                .HasForeignKey(lb => lb.BookId);

            modelBuilder.Entity<Loan>()
                .Property(l => l.Status)
                .HasConversion(
                    v => v.ToString(), // Enum -> String
                    v => (LoanStatus)Enum.Parse(typeof(LoanStatus), v) // String -> Enum
                );

            base.OnModelCreating(modelBuilder);
        }

    }
}
