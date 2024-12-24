using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryManagerAPI.Domain.ValueObjects
{
    public class UserVO
    {
        [JsonConstructor]
        public UserVO(string name, string email)
        {
            Name = name;
            Email = email;
        }

        [Required(ErrorMessage = "Name is required")] // Validação de campo obrigatório ao realizar o POST
        [StringLength(150, ErrorMessage = "Name must have a maximum of 150 characters")] // Validação de tamanho máximo
        public string Name { get; }

        [Required(ErrorMessage = "Email is required")] // Validação de campo obrigatório ao realizar o POST
        [EmailAddress(ErrorMessage = "Invalid email")] // Validação de formato de email
        public string Email { get; }
    }
}
