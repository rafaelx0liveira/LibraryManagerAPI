using LibraryManagerAPI.Domain.Entities;
using LibraryManagerAPI.Domain.Entities.Utils;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagerAPI.Infrastructure.Context
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

            // Populando o banco com HasData
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Year = 1925, Quantity = 3, ISBN = "9780743273565" },
                new Book { Id = 2, Title = "1984", Author = "George Orwell", Year = 1949, Quantity = 5, ISBN = "9780451524935" }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com" },
                new User { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com" }
            );

            base.OnModelCreating(modelBuilder);
        }

    }
}
