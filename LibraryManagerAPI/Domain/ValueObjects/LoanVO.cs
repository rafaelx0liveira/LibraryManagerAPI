using LibraryManagerAPI.Domain.Entities.Utils;

namespace LibraryManagerAPI.Domain.ValueObjects
{
    public class LoanVO
    {
        public long UserId { get; }
        public long BookId { get; }
        public DateTime Date { get; }
        public DateTime ReturnDate { get; }
        public BookingStatus Status { get; }
    }
}
