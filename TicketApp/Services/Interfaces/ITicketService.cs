using TicketApp.Models.DTOs;

namespace TicketApp.Services.Interfaces
{
    public interface ITicketService
    {
        /// <summary>
        /// Récupère toutes les entrées Tickets.
        /// </summary>
        Task<IEnumerable<GetTicketDto>> GetAllAsync();

        /// <summary>
        /// Récupère une Ticket par son identifiant.
        /// </summary>
        Task<GetTicketDto?> GetByIdAsync(int id);

        /// <summary>
        /// Crée une nouvelle entrée Ticket et renvoie l'app au Client.
        /// </summary>
        Task<GetTicketDto> CreateAsync(CreateTicketDto appDto);

        /// <summary>
        /// Met à jour l'entrée Tcket à l'identifiant spécifié
        /// </summary>
        Task<GetTicketDto?> UpdateAsync(int id, UpdateTicketDto appdto);

        /// <summary>
        /// Supprime l'entrée Ticket à l'identifiant spécifié
        /// </summary>
        Task<bool> DeleteAsync(int id);

    }
}
