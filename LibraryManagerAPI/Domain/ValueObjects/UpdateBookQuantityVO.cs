using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryManagerAPI.Domain.ValueObjects
{
    public class UpdateBookQuantityVO
    {
        // Constructor for Swagger doc
        [JsonConstructor]
        public UpdateBookQuantityVO(string isbn, int quantity)
        {
            ISBN = isbn;
            Quantity = quantity;
        }

        [Required(ErrorMessage = "ISBN is required")]
        [StringLength(50, ErrorMessage = "ISBN must have a maximum of 50 characters")]
        public string ISBN { get; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero")]
        public int Quantity { get; }
    }
}
