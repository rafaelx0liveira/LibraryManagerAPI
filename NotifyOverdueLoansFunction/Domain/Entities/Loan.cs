using HandleOverdueLoansFunction.Domain.Entities.Base;
using HandleOverdueLoansFunction.Domain.Entities.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandleOverdueLoansFunction.Domain.Entities
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
