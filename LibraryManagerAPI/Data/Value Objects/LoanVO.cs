namespace LibraryManagerAPI.Data.Value_Objects
{
    public class LoanVO
    {
        public long UserId { get; set; }
        public long BookId { get; set; }
        public DateTime Date { get; set; }
        public DateTime ReturnDate { get; set; }
        public BookingStatus Status { get; set; }
    }
}
