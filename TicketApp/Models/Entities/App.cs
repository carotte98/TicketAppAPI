using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketApp.Models.Entities
{
    [Table("App")]
    public class App
    {
        [Key]
        [Column("idApp")]
        public int id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("nameApp")]
        public string name { get; set; }

    }
}
