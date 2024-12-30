using LibraryManagerAPI.Domain.Entities.Utils;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryManagerAPI.Domain.ValueObjects.Input
{
    public class LoanVO
    {
        [JsonConstructor]
        public LoanVO(string email, string isbn)
        {
            Email = email;
            ISBN = isbn;
        }

        [Required(ErrorMessage = "Email is required")] // Validação de campo obrigatório ao realizar o POST
        [EmailAddress(ErrorMessage = "Invalid email")] // Validação de formato de email
        public string Email { get; }

        [Required(ErrorMessage = "ISBN is required")]
        [StringLength(50, ErrorMessage = "ISBN must have a maximum of 50 characters")]
        public string ISBN { get; }
    }
}
