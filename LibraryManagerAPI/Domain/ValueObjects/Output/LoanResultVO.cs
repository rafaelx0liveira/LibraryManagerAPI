using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace LibraryManagerAPI.Domain.ValueObjects.Output
{
    public class LoanResultVO
    {
        [JsonConstructor]
        public LoanResultVO(string userName, string bookTitle, string returnDate)
        {
            UserName = userName;
            BookTitle = bookTitle;
            ReturnDate = returnDate;
        }

        public string UserName { get; }
        public string BookTitle { get; }
        public string ReturnDate { get; }
    }

}
