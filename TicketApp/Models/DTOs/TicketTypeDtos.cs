using System.ComponentModel.DataAnnotations;

namespace TicketApp.Models.DTOs
{
    /// <summary>
    /// Classe TicketTypeDto, objet de transfer Objet utilisé pour la création et 
    /// la mise à jour
    /// </summary>
    public class TicketTypeDto
    {
        public int? idTicketType { get; set; } = null;

        [Required(ErrorMessage = "Le nom du type est obligatoire")]
        [StringLength(20, MinimumLength = 3,
            ErrorMessage = "Le nom du type doit contenir entre 3 et 20 caractères")]
        public string nameTicketType { get; set; } = string.Empty;
    }
}
