using AutoMapper;
using LibraryManagerAPI.Domain.Entities;
using LibraryManagerAPI.Domain.Entities.Utils;
using LibraryManagerAPI.Domain.Exceptions.BookExceptions;
using LibraryManagerAPI.Domain.Exceptions.LoanExceptions;
using LibraryManagerAPI.Domain.Exceptions.UserExceptions;
using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Domain.ValueObjects.Output;
using LibraryManagerAPI.Infrastructure.Context;
using LibraryManagerAPI.Presentation.Interfaces.Repository.Loan;
using LibraryManagerAPI.Presentation.Interfaces.Repository.User;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LibraryManagerAPI.Infrastructure.Persistance.Repositories
{
    public class LoanRepository(MySQLContext context, IMapper mapper) : 
        ILoanRepository
    {
        private readonly MySQLContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<LoanResultVO> RegisterLoanAsync(BookResultVO bookResultVO, UserResultVO userResultVO) 
        {
            Book book = _mapper.Map<Book>(bookResultVO);

            Loan loan = new Loan
            {
                UserId = userResultVO.Id,
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

            _context.Loans.Add(loan);
            _context.Books.Update(book);

            await _context.SaveChangesAsync();

            return new LoanResultVO(userResultVO.Name, book.Title, loan.ReturnDate.ToString("dd/MM/yyyy"));
        }

        public async Task<IEnumerable<LoanFromAUserResultVO>> GetLoanFromAUserAsync(string email)
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

        public async Task<LoanFromAUserResultVO> ReturnBookAndCloseLoanAsync(UserResultVO userResultVO, string isbn)
        {
            User user = _mapper.Map<User>(userResultVO);

            Book? book = await _context.Books.Where(x => x.ISBN == isbn).FirstOrDefaultAsync();

            Loan? loan = await _context.Loans
                .Include(ln => ln.User)
                .Include(ln => ln.LoanBooks)
                    .ThenInclude(lb => lb.Book)
                .Where(ln => ln.User.Id == user.Id &&
                        ln.LoanBooks.Any(lb => lb.Book.ISBN == book.ISBN))
                .FirstOrDefaultAsync();

            book.Quantity++;

            loan.Status = LoanStatus.GiveBack;
            _context.Loans.Update(loan);
            _context.Books.Update(book);

            await _context.SaveChangesAsync();

            return new LoanFromAUserResultVO(
                loan.LoanBooks.First().Book.Title,
                loan.LoanBooks.First().Book.ISBN,
                loan.User.Name,
                loan.User.Email,
                loan.Date.ToString("dd/MM/yyyy"),
                loan.ReturnDate.ToString("dd/MM/yyyy"),
                loan.Status.ToString()
            );
        }
    }
}
