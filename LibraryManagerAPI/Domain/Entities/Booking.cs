using LibraryManagerAPI.Domain.Entities.Base;
using LibraryManagerAPI.Domain.Entities.Utils;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagerAPI.Domain.Entities
{
    [Table("Booking")]
    public class Booking : BaseEntity
    {
        [ForeignKey("User")]
        public long UserId { get; set; }

        public virtual User User { get; set; } // Propriedade de navegação

        [ForeignKey("Book")]
        public long BookId { get; set; }

        public virtual Book Book { get; set; } // Propriedade de navegação

        [Column("Date")]
        public DateTime Date { get; set; }

        [Column("ReturnDate")]
        public DateTime ReturnDate { get; set; }

        [Column("Status")]
        public BookingStatus Status { get; set; }
    }

}
