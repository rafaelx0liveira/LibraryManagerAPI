using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace HandleOverdueLoansFunction.Domain.ValueObjects.Output
{
    public class OverdueLoansResultVO
    {
        [JsonConstructor]
        public OverdueLoansResultVO(string userEmail, string userName, string bookTitle, string returnDate)
        {
            UserEmail = userEmail;
            UserName = userName;
            BookTitle = bookTitle;
            ReturnDate = returnDate;
        }
        public string UserEmail { get; }

        public string UserName { get; }
        public string BookTitle { get; }
        public string ReturnDate { get; }
    }
}
