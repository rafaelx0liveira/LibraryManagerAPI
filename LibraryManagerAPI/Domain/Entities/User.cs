using LibraryManagerAPI.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagerAPI.Domain.Entities
{
    [Table("User")]
    public class User : BaseEntity
    {
        [Column("Name")]
        [StringLength(150)]
        public string? Name { get; set; }

        [Column("Email")]
        public string? Email { get; set; }

        public virtual ICollection<Loan> Loans { get; set; } // Propriedade de navegação
    }

}
