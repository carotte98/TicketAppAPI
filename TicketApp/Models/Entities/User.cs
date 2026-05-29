using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketApp.Models.Entities
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("idUser")]
        public int id { get; set; }

        [Required]
        [MaxLength(40)]
        [Column("nameuser")]
        public string name { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("emailuser")]
        public string email { get; set; }

        [Required]
        [MaxLength(60)]
        [Column("passworduser")]
        public string password { get; set; }

        [Required]
        [Column("firstconnexion")]
        public bool conn {  get; set; }

        [Required]
        [Column("idrole")]
        public int idRole { get; set; }

        [ForeignKey(nameof(idRole))]
        public Role role { get; set; }


    }
}
