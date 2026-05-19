using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketApp.Models.Entities
{
    [Table("status")]
    public class Status
    {
        [Key]
        [Column("idstatus")]
        public int id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("namestatus")]
        public string name { get; set; }
    }
}
