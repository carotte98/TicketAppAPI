using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketApp.Models.Entities
{
    [Table("role")]
    public class Role
    {
        [Key]
        [Column("idrole")]
        public int id { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("namerole")]
        public string name { get; set; }


    }
}
