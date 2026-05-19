using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketApp.Models.Entities
{
    [Table("tickettype")]
    public class TicketType
    {        
        [Key]
        [Column("idtickettype")]
        public int id { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("nametickettype")]
        public string name { get; set; }

        
    }
}
