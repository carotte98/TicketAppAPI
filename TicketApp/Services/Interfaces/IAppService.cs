using TicketApp.Models.DTOs;

namespace TicketApp.Services.Interfaces
{
    public interface IAppService
    {
        /// <summary>
        /// Récupère toutes les entrées Apps.
        /// </summary>
        Task<IEnumerable<AppDto>> GetAllAsync();

        /// <summary>
        /// Récupère une app par son identifiant.
        /// </summary>
        Task<AppDto?> GetByIdAsync(int id);

        /// <summary>
        /// Crée une nouvelle entrée app et renvoie l'app au Client.
        /// </summary>
        Task<AppDto> CreateAsync(AppDto appDto);

        /// <summary>
        /// Met à jour l'entrée app à l'identifiant spécifié
        /// </summary>
        Task<AppDto?> UpdateAsync(int id, AppDto appdto);

        /// <summary>
        /// Supprime l'entrée pp à l'identifiant spécifié
        /// </summary>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Contrôle si l'application existe à ce nom
        /// </summary>
        Task<bool> ExistsByName(string name);
    }
}
