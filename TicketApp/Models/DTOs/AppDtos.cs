namespace TicketApp.Models.DTOs
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Classe AppDto, objet de transfer Objet utilisé pour la création et 
    /// la mise à jour
    /// </summary>
    public class AppDto
    {
        public int? idApp { get; set; } = null;

        [Required(ErrorMessage = "Le nom de l'application est obligatoire")]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "Le nom de l'application doit contenir entre 3 et 50 caractères")]
        public string nameApp {  get; set; } = string.Empty;
    }
}
