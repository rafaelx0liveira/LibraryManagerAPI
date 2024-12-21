using LibraryManagerAPI.Model.Base;
using LibraryManagerAPI.Model.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagerAPI.Model
{
    [Table("Loan")]
    public class Loan : BaseEntity
    {
        [ForeignKey("User")]
        public long UserId { get; set; }

        public virtual User User { get; set; } // Propriedade de navegação

        [Column("Date")]
        public DateTime Date { get; set; }

        [Column("ReturnDate")]
        public DateTime ReturnDate { get; set; }

        [Column("Status")]
        public LoanStatus Status { get; set; }

        public virtual ICollection<LoanBook> LoanBooks { get; set; } // Relacionamento N:M com Book
    }

}
