using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketApp.Models.Entities
{
    [Table("app")]
    public class App
    {
        [Key]
        [Column("idapp")]
        public int id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("nameapp")]
        public string name { get; set; }

    }
}
