using LibraryManagerAPI.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagerAPI.Domain.Entities
{
    [Table("Book")]
    public class Book : BaseEntity
    {
        [Column("Title")]
        [StringLength(300)]
        public string? Title { get; set; }

        [Column("Author")]
        [StringLength(150)]
        public string? Author { get; set; }

        [Column("ISBN")]
        [StringLength(50)]
        public string? ISBN { get; set; }

        [Column("Year")]
        public int Year { get; set; }

        [Column("Language")]
        public string? Language { get; set; }

        [Column("Quantity")]
        public int Quantity { get; set; }

        public virtual ICollection<LoanBook> LoanBooks { get; set; } // Relacionamento N:M com Loan
    }

}
