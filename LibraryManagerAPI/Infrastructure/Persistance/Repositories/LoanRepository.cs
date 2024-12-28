using AutoMapper;
using LibraryManagerAPI.Domain.Entities;
using LibraryManagerAPI.Domain.Entities.Utils;
using LibraryManagerAPI.Domain.Exceptions.BookExceptions;
using LibraryManagerAPI.Domain.Exceptions.UserExceptions;
using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Domain.ValueObjects.Output;
using LibraryManagerAPI.Infrastructure.Context;
using LibraryManagerAPI.Presentation.Interfaces.Repository.Loan;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LibraryManagerAPI.Infrastructure.Persistance.Repositories
{
    public class LoanRepository(MySQLContext context, IMapper mapper) : 
        ILoanRepository
    {
        private readonly MySQLContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<LoanResultVO> RegisterLoan(LoanVO loan)
        {
            User user = _context.Users.Where(x => x.Email == loan.UserEmail).FirstOrDefault() ?? new User();

            if (user.Email == null)
            {
                throw new UserNotFoundException($"User with email '{loan.UserEmail}' was not found.");
            }

            Book book = _context.Books.Where(x => x.ISBN == loan.ISBN).FirstOrDefault() ?? new Book();

            if (book.ISBN == null || book.Quantity == 0)
            {
                throw new BookNotAvailableException($"The book with ISBN '{loan.ISBN}' is not available.");
            }

            Loan newLoan = new Loan
            {
                UserId = user.Id,
                Date = DateTime.Now,
                ReturnDate = DateTime.Now.AddMonths(1),
                Status = LoanStatus.Active,
                LoanBooks = new List<LoanBook>
                {
                    new LoanBook
                    {
                        BookId = book.Id,
                    }
                }
            };

            book.Quantity--;

            _context.Loans.Add(newLoan);
            _context.Books.Update(book);

            await _context.SaveChangesAsync();

            return new LoanResultVO(user.Name, book.Title, newLoan.ReturnDate.ToString("dd/MM/yyyy"));
        }

        public async Task<IEnumerable<LoanFromAUserResultVO>> GetLoanFromAUser(string email)
        {
            IEnumerable<LoanFromAUserResultVO> loanResults = _context.Loans
                .Include(ln => ln.User) // Inclui informações do usuário
                .Include(ln => ln.LoanBooks) // Inclui relação com LoanBooks
                    .ThenInclude(lb => lb.Book) // Inclui informações do Livro
                .Where(ln => ln.User.Email == email) // Filtra pelo usuário
                .Select(ln => new LoanFromAUserResultVO(
                   ln.LoanBooks.FirstOrDefault().Book.Title, // Título do primeiro livro
                   ln.LoanBooks.FirstOrDefault().Book.ISBN, // ISBN do primeiro livro
                   ln.User.Name,
                   ln.User.Email,
                   ln.Date.ToString("dd/MM/yyyy"),
                   ln.ReturnDate.ToString("dd/MM/yyyy"),
                   ln.Status.ToString()
                ))
                .ToList();

            return loanResults;
            
        }
    }
}
