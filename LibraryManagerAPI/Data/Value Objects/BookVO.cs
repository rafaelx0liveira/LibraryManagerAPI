using System.ComponentModel.DataAnnotations;

namespace LibraryManagerAPI.Data.Value_Objects
{
    public class BookVO
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(300, ErrorMessage = "Title must have a maximum of 300 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        [StringLength(150, ErrorMessage = "Author must have a maximum of 150 characters")]
        public string Author { get; set; }

        [Required(ErrorMessage = "ISBN is required")]
        [StringLength(50, ErrorMessage = "ISBN must have a maximum of 50 characters")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Year is required")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Language is required")]
        [StringLength(50, ErrorMessage = "Language must have a maximum of 50 characters")]
        public string Language { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
    }
}
