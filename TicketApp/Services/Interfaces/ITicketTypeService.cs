using TicketApp.Models.DTOs;

namespace TicketApp.Services.Interfaces
{
    public interface ITicketTypeService
    {
        /// <summary>
        /// Récupère toutes les entrées Tickettype.
        /// </summary>
        Task<IEnumerable<TicketTypeDto>> GetAllAsync();

        /// <summary>
        /// Récupère une TicketType par son identifiant.
        /// </summary>
        Task<TicketTypeDto?> GetByIdAsync(int id);

        /// <summary>
        /// Crée une nouvelle entrée TicketType et renvoie l'app au Client.
        /// </summary>
        Task<TicketTypeDto> CreateAsync(TicketTypeDto typeDto);

        /// <summary>
        /// Met à jour l'entrée TicketType à l'identifiant spécifié
        /// </summary>
        Task<TicketTypeDto?> UpdateAsync(int id, TicketTypeDto typedto);

        /// <summary>
        /// Supprime l'entrée TicketType à l'identifiant spécifié
        /// </summary>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Contrôle si le TicketType existe à ce nom
        /// </summary>
        Task<bool> ExistsByName(string name);
    }
}
