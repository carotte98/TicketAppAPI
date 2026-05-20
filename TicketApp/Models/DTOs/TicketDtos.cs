using System.ComponentModel.DataAnnotations;
using TicketApp.Models.Entities;

namespace TicketApp.Models.DTOs
{
    /// <summary>
    /// DTO pour les requêtes Get
    /// </summary>
    public class GetTicketDto
    {
        // L'id du Ticket
        [Required(ErrorMessage = "L'id du Ticket est obligeatoire")]
        public int idTicket { get; set; }

        // Le nom du Ticket
        [Required(ErrorMessage = "Le nom du Ticket est obligatoire")]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "Le nom du Ticket doit contenir entre 5 et 60 caractères")]
        public string nameTicket { get; set; } = string.Empty;

        // L'utilisateur qui a fait le Ticket
        [Required(ErrorMessage = "L'auteur du Ticket est obligatoire")]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "L'auteur du Ticket doit contenir entre 3 et 40 caractères")]
        public string authorTicket { get; set; } = string.Empty;

        // Le message de l'utilisateur
        [Required(ErrorMessage = "Le message utilisateur du Ticket est obligatoire")]
        [StringLength(255, MinimumLength = 5,
            ErrorMessage = "Le message utilisateur du Ticket doit contenir entre 5 et 255 caractères")]
        public string authorMsgTicket { get; set; } = string.Empty;

        // Le dev responsable du Ticket
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "Le responsable du Ticket doit contenir entre 3 et 40 caractères")]
        public string? devTicket { get; set; } = string.Empty;

        // Le message du dev responsable du Ticket
        [StringLength(255, MinimumLength = 5,
            ErrorMessage = "Le message du responsable du Ticket doit contenir entre 5 et 255 caractères")]
        public string? devMsgTicket { get; set; } = string.Empty;

        // La date de départ du Ticket
        [Required(ErrorMessage = "La date de départ du Ticket est obligatoire")]
        public DateTime startdateTicket { get; set; }

        // La date de la derrnière mise à jour du Ticket
        [Required(ErrorMessage = "La date de mise à jour du Ticket est obligatoire")]
        public DateTime updateDateTicket { get; set; }

        // Le Statut du Ticket
        [Required(ErrorMessage = "Le status du Ticket est obligeatoire")]
        public Status statusTicket { get; set; }

        // Le Type du ticket
        [Required(ErrorMessage = "Le type du Ticket est obligeatoire")]
        public TicketType typeTicket { get; set; }

        // L'application que le Ticket concerne
        [Required(ErrorMessage = "L'application du Ticket est obligeatoire")]
        public App appTicket { get; set; }

    }

    /// <summary>
    /// DTO pour les requêtes Put
    /// </summary>
    public class UpdateTicketDto
    {
        // Le message de l'utilisateur
        [Required(ErrorMessage = "Le message utilisateur du Ticket est obligatoire")]
        [StringLength(255, MinimumLength = 5,
            ErrorMessage = "Le message utilisateur du Ticket doit contenir entre 5 et 255 caractères")]
        public string authorMsgTicket { get; set; } = string.Empty;

        // Le dev responsable du Ticket
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "Le responsable du Ticket doit contenir entre 3 et 40 caractères")]
        public string? devTicket { get; set; } = string.Empty;

        // Le message du dev responsable du Ticket
        [StringLength(255, MinimumLength = 5,
            ErrorMessage = "Le message du responsable du Ticket doit contenir entre 5 et 255 caractères")]
        public string? devMsgTicket { get; set; } = string.Empty;

        // La date de la derrnière mise à jour du Ticket
        [Required(ErrorMessage = "La date de mise à jour du Ticket est obligatoire")]
        public DateTime updateDateTicket { get; set; }

        // Le Statut du Ticket
        [Required(ErrorMessage = "Le status du Ticket est obligeatoire")]
        public Status statusTicket { get; set; }

        // Le Type du ticket
        [Required(ErrorMessage = "Le type du Ticket est obligeatoire")]
        public TicketType typeTicket { get; set; }

    }


    /// <summary>
    /// DTO pour les requêtes Post
    /// </summary>
    public class CreateTicketDto
    {
        // Le nom du Ticket
        [Required(ErrorMessage = "Le nom du Ticket est obligatoire")]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "Le nom du Ticket doit contenir entre 5 et 60 caractères")]
        public string nameTicket { get; set; } = string.Empty;

        // L'utilisateur qui a fait le Ticket
        [Required(ErrorMessage = "L'auteur du Ticket est obligatoire")]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "L'auteur du Ticket doit contenir entre 3 et 40 caractères")]
        public string authorTicket { get; set; } = string.Empty;

        // Le message de l'utilisateur
        [Required(ErrorMessage = "Le message utilisateur du Ticket est obligatoire")]
        [StringLength(255, MinimumLength = 5,
            ErrorMessage = "Le message utilisateur du Ticket doit contenir entre 5 et 255 caractères")]
        public string authorMsgTicket { get; set; } = string.Empty;

        // La date de départ du Ticket
        [Required(ErrorMessage = "La date de départ du Ticket est obligatoire")]
        public DateTime startdateTicket { get; set; }

        // La date de la derrnière mise à jour du Ticket
        [Required(ErrorMessage = "La date de mise à jour du Ticket est obligatoire")]
        public DateTime updateDateTicket { get; set; }

        // Le Statut du Ticket
        [Required(ErrorMessage = "Le status du Ticket est obligeatoire")]
        public Status statusTicket { get; set; }

        // Le Type du ticket
        [Required(ErrorMessage = "Le type du Ticket est obligeatoire")]
        public TicketType typeTicket { get; set; }

        // L'application que le Ticket concerne
        [Required(ErrorMessage = "L'application du Ticket est obligeatoire")]
        public App appTicket { get; set; }

    }
}
