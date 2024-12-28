using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryManagerAPI.Domain.ValueObjects.Input
{
    public class BookVO
    {
        // Constructor for Swagger doc
        [JsonConstructor]
        public BookVO(string title, string author, string isbn, int year, string language, int quantity)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            Year = year;
            Language = language;
            Quantity = quantity;
        }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(300, ErrorMessage = "Title must have a maximum of 300 characters")]
        public string Title { get; }

        [Required(ErrorMessage = "Author is required")]
        [StringLength(150, ErrorMessage = "Author must have a maximum of 150 characters")]
        public string Author { get; }

        [Required(ErrorMessage = "ISBN is required")]
        [StringLength(50, ErrorMessage = "ISBN must have a maximum of 50 characters")]
        public string ISBN { get; }

        [Required(ErrorMessage = "Year is required")]
        public int Year { get; }

        [Required(ErrorMessage = "Language is required")]
        [StringLength(50, ErrorMessage = "Language must have a maximum of 50 characters")]
        public string Language { get; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero")]
        public int Quantity { get; set; }
    }
}
