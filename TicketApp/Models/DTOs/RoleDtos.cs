using System.ComponentModel.DataAnnotations;

namespace TicketApp.Models.DTOs
{
    /// <summary>
    /// Data Transfer Object pour le rôle des utilisateurs
    /// </summary>
    public class RoleDto
    {
        public int? idRole { get; set; } = null;

        [Required(ErrorMessage = "Le nom du rôle est obligatoire")]
        [StringLength(20, MinimumLength = 2,
            ErrorMessage = "Le nom du rôle doit contenir entre 2 et 20 caractères")]
        public string nameRole { get; set; } = string.Empty;
    }
}
