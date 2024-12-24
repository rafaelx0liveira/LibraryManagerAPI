using AutoMapper;
using LibraryManagerAPI.Domain.Entities;
using LibraryManagerAPI.Domain.ValueObjects;
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

        public async Task<IEnumerable<BookVO>> RegisterBooks(IEnumerable<BookVO> bookVOs)
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

            return _mapper.Map<IEnumerable<BookVO>>(booksToAdd.Concat(booksToUpdate));
        }

        public async Task<IEnumerable<BookVO>> GetAllBooks()
        {
            return _mapper.Map<IEnumerable<BookVO>>(await _context.Books.Where(x => x.Quantity > 0).ToListAsync());
        }

        public async Task<IEnumerable<BookVO>> GetBookByTitle(string title)
        {
            return _mapper.Map<IEnumerable<BookVO>>(await _context.Books.Where(x => x.Title == title && x.Quantity > 0).ToListAsync());
        }

        public async Task<IEnumerable<BookVO>> GetBookByAuthor(string author)
        {
            return _mapper.Map<IEnumerable<BookVO>>(await _context.Books.Where(x => x.Author == author && x.Quantity > 0).ToListAsync());
        }

        public async Task<BookVO> GetBookByISBN(string isbn)
        {
            return _mapper.Map<BookVO>(await _context.Books.FirstOrDefaultAsync(x => x.ISBN == isbn && x.Quantity > 0));
        }

        public async Task<bool> MarkBookAsUnavailable(string isbn)
        {
            try
            {
                Book book = await _context.Books.Where(x => x.ISBN == isbn).FirstOrDefaultAsync() ?? new Book();

                if (book.ISBN == null) return false;

                book.Quantity = 0;

                _context.Books.UpdateRange(book);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }   
        }

        public async Task<bool> UpdateBookQuantity(UpdateBookQuantityVO updateBookQuantityVO)
        {
            try
            {
                Book book = await _context.Books.Where(x => x.ISBN == updateBookQuantityVO.ISBN).FirstOrDefaultAsync() ?? new Book();

                if (book.ISBN == null) return false;

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
    }
}
