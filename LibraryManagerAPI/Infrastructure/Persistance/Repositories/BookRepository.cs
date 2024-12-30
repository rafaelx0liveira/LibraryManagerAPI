using AutoMapper;
using LibraryManagerAPI.Domain.Entities;
using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Domain.ValueObjects.Output;
using LibraryManagerAPI.Infrastructure.Context;
using LibraryManagerAPI.Presentation.Interfaces.Repository.Book;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagerAPI.Infrastructure.Persistance.Repositories
{
    public class BookRepository(MySQLContext context, IMapper mapper) 
        : IBookRepository
    {
        private readonly MySQLContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<BookResultVO>> RegisterBooksAsync(IEnumerable<BookVO> bookVOs)
        {
            var booksToUpdate = new List<Book>();
            var booksToAdd = new List<Book>();

            IEnumerable<Book> incomingBooks = _mapper.Map<IEnumerable<Book>>(bookVOs);

            foreach (var incomingBook in incomingBooks)
            {
                var existingBook = await _context.Books
                    .FirstOrDefaultAsync(b => b.Title == incomingBook.Title && b.Author == incomingBook.Author);

                if (existingBook != null)
                {
                    existingBook.Quantity += incomingBook.Quantity;
                    booksToUpdate.Add(existingBook);
                }
                else
                {
                    booksToAdd.Add(incomingBook);
                }
            }

            if (booksToUpdate.Any())
                _context.Books.UpdateRange(booksToUpdate); 

            if (booksToAdd.Any())
                _context.Books.AddRange(booksToAdd); 

            await _context.SaveChangesAsync();

            return _mapper.Map<IEnumerable<BookResultVO>>(booksToAdd.Concat(booksToUpdate));
        }

        public async Task<IEnumerable<BookResultVO>> GetAllBooksAsync()
        {
            return _mapper.Map<IEnumerable<BookResultVO>>(await _context.Books.Where(x => x.Quantity > 0).ToListAsync());
        }

        public async Task<IEnumerable<BookResultVO>> GetBookByTitleAsync(string title)
        {
            return _mapper.Map<IEnumerable<BookResultVO>>(await _context.Books.AsNoTracking().Where(x => x.Title == title && x.Quantity > 0).ToListAsync());
        }

        public async Task<IEnumerable<BookResultVO>> GetBookByAuthorAsync(string author)
        {
            return _mapper.Map<IEnumerable<BookResultVO>>(await _context.Books.AsNoTracking().Where(x => x.Author == author && x.Quantity > 0).ToListAsync());
        }

        public async Task<BookResultVO> GetBookByISBNAsync(string isbn)
        {
            return _mapper.Map<BookResultVO>(await _context.Books.AsNoTracking().FirstOrDefaultAsync(x => x.ISBN == isbn && x.Quantity > 0));
        }

        public async Task<bool> MarkBookAsUnavailableAsync(string isbn)
        {
            try
            {
                Book book = await _context.Books.Where(x => x.ISBN == isbn).FirstOrDefaultAsync() ?? new Book();

                book.Quantity = 0;

                _context.Books.UpdateRange(book);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }   
        }

        public async Task<bool> UpdateBookQuantityAsync(UpdateBookQuantityVO updateBookQuantityVO)
        {
            try
            {
                Book book = await _context.Books.Where(x => x.ISBN == updateBookQuantityVO.ISBN).FirstOrDefaultAsync() ?? new Book();

                book.Quantity += updateBookQuantityVO.Quantity;

                _context.Books.UpdateRange(book);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> IsBookAvailableAsync(string isbn)
        {
            return await _context.Books.Where(x => x.ISBN == isbn && x.Quantity > 0).AnyAsync();
        }

        public async Task<bool> BookExistsAsync(string isbn)
        {
            return await _context.Books.AnyAsync(x => x.ISBN == isbn);
        }

        public async Task<bool> IsBookAlreadyLoanedByUserAsync(string userEmail, string isbn)
        {
            return await _context.Loans
                .Include(l => l.LoanBooks)
                .Include(l => l.User)
                .AnyAsync(l => l.User.Email == userEmail && l.LoanBooks.Any(lb => lb.Book.ISBN == isbn));
        }
    }
}
