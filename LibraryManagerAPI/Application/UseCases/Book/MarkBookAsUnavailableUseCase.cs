using LibraryManagerAPI.Application.Commom.Validation;
using LibraryManagerAPI.Presentation.Interfaces.Repository.Book;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.Book;

namespace LibraryManagerAPI.Application.UseCases.Book
{
    public class MarkBookAsUnavailableUseCase (IBookRepository libraryRepository)
        : IMarkBookAsUnavailableUseCase
    {
        private readonly IBookRepository _libraryRepository = libraryRepository;

        public async Task<bool> MarkBookAsUnavailable(string isbn)
        {
            ValidatorService.Validate(isbn);

            return await _libraryRepository.MarkBookAsUnavailable(isbn);
        }
    }
}
