using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagerAPI.Model
{
    [Table("LoanBook")]
    public class LoanBook
    {
        [ForeignKey("Loan")]
        public long LoanId { get; set; }

        public virtual Loan Loan { get; set; } // Propriedade de navegação

        [ForeignKey("Book")]
        public long BookId { get; set; }

        public virtual Book Book { get; set; } // Propriedade de navegação
    }

}
