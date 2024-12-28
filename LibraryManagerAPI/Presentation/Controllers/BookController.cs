using LibraryManagerAPI.Domain.Exceptions.BookExceptions;
using LibraryManagerAPI.Domain.Exceptions.ValidationFieldsExceptions;
using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.Book;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagerAPI.Presentation.Controllers
{
    /// <summary>
    /// Book Controller
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BookController(
            IRegisterBooksUseCase _registerBookUseCase, 
            IGetAllBooksUseCase _getAllBooksUseCase,
            IGetBookByTitleUseCase _getBookByTitleUseCase,
            IGetBookByAuthorUseCase _getBookByAuthorUseCase,
            IGetBookByISBNUseCase _getBookByISBNUseCase,
            IMarkBookAsUnavailableUseCase _markBookAsUnavailableUseCase,
            IUpdateBookQuantityUseCase _updateBookQuantityUseCase
        )
        : ControllerBase
    {
        private readonly IRegisterBooksUseCase _registerBookUseCase = _registerBookUseCase;
        private readonly IGetAllBooksUseCase _getAllBooksUseCase = _getAllBooksUseCase;
        private readonly IGetBookByTitleUseCase _getBookByTitleUseCase = _getBookByTitleUseCase;
        private readonly IGetBookByAuthorUseCase _getBookByAuthorUseCase = _getBookByAuthorUseCase;
        private readonly IGetBookByISBNUseCase _getBookByISBNUseCase = _getBookByISBNUseCase;
        private readonly IMarkBookAsUnavailableUseCase _markBookAsUnavailableUseCase = _markBookAsUnavailableUseCase;
        private readonly IUpdateBookQuantityUseCase _updateBookQuantityUseCase = _updateBookQuantityUseCase;


        /// <summary>
        /// Register a list of books
        /// </summary>
        /// <param name="bookVO">List of books to be registered</param>
        /// <response code="200">Returns the list of books registered</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="500">If an error occurs</response>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterBooks([FromBody] IEnumerable<BookVO> bookVO)
        {
            try
            {
                var result = await _registerBookUseCase.RegisterBooks(bookVO);
                return Ok(result);
            }
            catch (CustomValidationFieldsException ex)
            {
                return BadRequest(new {
                    Message = "Validation failed",
                    ex.Errors
                });
            }
        }

        /// <summary>
        /// Get all books
        /// </summary>
        /// <response code="200">Returns the list of books</response>
        /// <response code="500">If an error occurs</response>
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _getAllBooksUseCase.GetAllBooks();
            return Ok(result);
        }

        /// <summary>
        /// Get book by title
        /// </summary>
        /// <param name="title">Title of book</param>
        /// <response code="200">Returns book</response>
        /// <response code="500">If an error occurs</response>
        [HttpGet]
        [Route("title")]
        public async Task<IActionResult> GetBookByTitle([FromQuery] string title)
        {
            try
            {
                var result = await _getBookByTitleUseCase.GetBookByTitle(title);
                return Ok(result);
            }
            catch (CustomValidationFieldsException ex)
            {
                // Return the validation error (500)
                return BadRequest(new
                {
                    message = "Validation failed",
                    errors = ex.Errors
                });
            }
            catch (BookNotAvailableException ex)
            {
                // Return resource not found (404 Not Found)
                return NotFound(new
                {
                    ex.Message
                });
            }
        }

        /// <summary>
        /// Get book by author
        /// </summary>
        /// <param name="author">Author's name</param>
        /// <response code="200">Returns author's books</response>
        /// <response code="500">If an error occurs</response>
        [HttpGet]
        [Route("author")]
        public async Task<IActionResult> GetBookByAuthor([FromQuery] string author)
        {
            try
            {
                var result = await _getBookByAuthorUseCase.GetBookByAuthor(author);

                return Ok(result);
            }
            catch (CustomValidationFieldsException ex)
            {
                // Return the validation error (500)
                return BadRequest(new
                {
                    message = "Validation failed",
                    errors = ex.Errors
                });
            }
            catch (BookNotAvailableException ex)
            {
                // Return resource not found (404 Not Found)
                return NotFound(new
                {
                    ex.Message
                });
            }
        }

        /// <summary>
        /// Get book by ISBN
        /// </summary>
        /// <param name="isbn">International Standard Book Number</param>
        /// <response code="200">Return a book</response>
        /// <response code="500">If an error occurs</response>
        [HttpGet]
        [Route("ISBN")]
        public async Task<IActionResult> GetBookByISBN([FromQuery] string isbn)
        {
            var result = await _getBookByISBNUseCase.GetBookByISBN(isbn);

            return Ok(result);
        }

        /// <summary>
        /// Mark book as unavailable
        /// </summary>
        /// <param name="isbn">International Standard Book Number</param>
        /// <response code="200">Book marked as unavailable</response>
        /// <response code="500">If an error occurs</response>
        [HttpPatch]
        [Route("markBookAsUnavailable")]
        public async Task<IActionResult> MarkBookAsUnavailable([FromQuery] string isbn)
        {
            var result = await _markBookAsUnavailableUseCase.MarkBookAsUnavailable(isbn);

            return Ok(result);
        }

        /// <summary>
        /// Update book quantity
        /// </summary>
        /// <param name="updateBookQuantityVO">International Standard Book Number and Quantity</param>
        /// <response code="200">Quantity updated</response>
        /// <response code="500">If an error occurs</response>
        [HttpPatch]
        [Route("updateQuantity")]
        public async Task<IActionResult> UpdateBookQuantity([FromBody]UpdateBookQuantityVO updateBookQuantityVO)
        {
            var result = await _updateBookQuantityUseCase.UpdateBookQuantity(updateBookQuantityVO);

            return Ok(result);
        }
    }
}
