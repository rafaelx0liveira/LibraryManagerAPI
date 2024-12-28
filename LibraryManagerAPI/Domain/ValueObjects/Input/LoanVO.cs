using LibraryManagerAPI.Domain.Entities.Utils;
using System.Text.Json.Serialization;

namespace LibraryManagerAPI.Domain.ValueObjects.Input
{
    public class LoanVO
    {
        [JsonConstructor]
        public LoanVO(string userEmail, string isbn)
        {
            UserEmail = userEmail;
            ISBN = isbn;
        }

        public string UserEmail { get; }
        public string ISBN { get; }
    }
}
