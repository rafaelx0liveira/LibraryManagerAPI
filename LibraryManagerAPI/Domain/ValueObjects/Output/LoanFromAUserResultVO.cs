using LibraryManagerAPI.Domain.Entities.Utils;
using System.Text.Json.Serialization;

namespace LibraryManagerAPI.Domain.ValueObjects.Output
{
    public class LoanFromAUserResultVO
    {
        [JsonConstructor]
        public LoanFromAUserResultVO(
            string title, string isbn, string userName, string userEmail,
            string date, string returnDate, string loanStatus)
        {
            Title = title;
            ISBN = isbn;
            UserName = userName;
            UserEmail = userEmail;
            Date = date;
            ReturnDate = returnDate;
            LoanStatus = loanStatus;
        }

        public string Title { get; }
        public string ISBN { get; }
        public string UserName { get; }
        public string UserEmail { get; }
        public string Date { get; }
        public string ReturnDate { get; }
        public string LoanStatus { get; }
    }
}
