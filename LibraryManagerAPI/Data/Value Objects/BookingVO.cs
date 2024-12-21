using LibraryManagerAPI.Model.Utils;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagerAPI.Data.Value_Objects
{
    public class BookingVO
    {
        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; private set; }

        [Required(ErrorMessage = "ReturnDate is required")]
        public DateTime ReturnDate { get; private set; }

        public BookingStatus Status { get; private set; }

        public BookingVO(DateTime date, DateTime returnDate, BookingStatus status)
        {
            if (date > returnDate)
                throw new ArgumentException("ReturnDate must be later than Date.");

            Date = date;
            ReturnDate = returnDate;
            Status = status;
        }

        public override string ToString()
        {
            return $"Booking from {Date:yyyy-MM-dd} to {ReturnDate:yyyy-MM-dd}, Status: {Status}";
        }
    }

}
