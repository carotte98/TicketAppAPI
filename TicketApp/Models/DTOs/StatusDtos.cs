using System.ComponentModel.DataAnnotations;

namespace TicketApp.Models.DTOs
{
    /// <summary>
    /// Classe StatusDto, objet de transfer Objet utilisé pour la création et 
    /// la mise à jour
    /// </summary>
    public class StatusDto
    {
        public int? idStatus { get; set; } = null;

        [Required(ErrorMessage = "Le nom du statut est obligatoire")]
        [StringLength(20, MinimumLength = 2,
            ErrorMessage = "Le nom de l'application doit contenir entre 2 et 20 caractères")]
        public string nameStatus { get; set; } = string.Empty;
    }
}
