using TicketApp.Models.DTOs;

namespace TicketApp.Services.Interfaces
{
    public interface IStatusService
    {
        /// <summary>
        /// Récupère toutes les entrées statuts.
        /// </summary>
        Task<IEnumerable<StatusDto>> GetAllAsync();

        /// <summary>
        /// Récupère un statut par son identifiant.
        /// </summary>
        Task<StatusDto?> GetByIdAsync(int id);

        /// <summary>
        /// Crée une nouvelle entrée app et renvoie l'statut au Client.
        /// </summary>
        Task<StatusDto> CreateAsync(StatusDto statusDto);

        /// <summary>
        /// Met à jour l'entrée statut à l'identifiant spécifié
        /// </summary>
        Task<StatusDto?> UpdateAsync(int id, StatusDto statusdto);

        /// <summary>
        /// Supprime l'entrée statut à l'identifiant spécifié
        /// </summary>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Contrôle si le statut existe à ce nom
        /// </summary>
        Task<bool> ExistsByName(string name);
    }
}
