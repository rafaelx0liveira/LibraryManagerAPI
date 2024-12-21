using System.ComponentModel.DataAnnotations;

namespace LibraryManagerAPI.Data.Value_Objects
{
    public class UserVO
    {
        [Required(ErrorMessage = "Name is required")] // Validação de campo obrigatório ao realizar o POST
        [StringLength(150, ErrorMessage = "Name must have a maximum of 150 characters")] // Validação de tamanho máximo
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")] // Validação de campo obrigatório ao realizar o POST
        [EmailAddress(ErrorMessage = "Invalid email")] // Validação de formato de email
        public string Email { get; set; }
    }
}
