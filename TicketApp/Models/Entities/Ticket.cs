using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketApp.Models.Entities
{
    [Table("ticket")]
    public class Ticket
    {
        [Key]
        [Column("idticket")]
        public int id { get; set; }


        [Required]
        [MaxLength(60)]
        [Column("nameticket")]
        public string name { get; set; }


        [Required]
        [MaxLength(40)]
        [Column("authorticket")]
        public string authorName { get; set; }


        [Required]
        [MaxLength(255)]
        [Column("authormsgticket")]
        public string authorMsg { get; set; }


        [MaxLength(40)]
        [Column("devticket")]
        public string devName { get; set; }


        [MaxLength(255)]
        [Column("devmsgticket")]
        public string devMsg { get; set; }


        [Required]
        [Column("startdateticket")]
        public DateTime startDate { get; set; }


        [Required]
        [Column("updatedateticket")]
        public DateTime updateDate { get; set; }


        [Required]
        [Column("idstatus")]
        public int idStatus { get; set; }

        [ForeignKey("idstatus")]
        public Status status { get; set; }


        [Required]
        [Column("idtickettype")]
        public int idTicketType { get; set; }

        [ForeignKey("idtickettype")]
        public TicketType ticketType { get; set; }


        [Required]
        [Column("idapp")]
        public int idApp { get; set; }

        [ForeignKey("idapp")]
        public App app { get; set; }





    }
}
