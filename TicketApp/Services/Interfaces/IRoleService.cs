using TicketApp.Models.DTOs;

namespace TicketApp.Services.Interfaces
{
    public interface IRoleService
    {
        /// <summary>
        /// Récupère toutes les entrées roles.
        /// </summary>
        Task<IEnumerable<RoleDto>> GetAllAsync();

        /// <summary>
        /// Récupère un role par son identifiant.
        /// </summary>
        Task<RoleDto?> GetByIdAsync(int id);

        /// <summary>
        /// Crée une nouvelle entrée rôle et renvoie l'statut au Client.
        /// </summary>
        Task<RoleDto> CreateAsync(RoleDto roleDto);

        /// <summary>
        /// Met à jour l'entrée role à l'identifiant spécifié
        /// </summary>
        Task<RoleDto?> UpdateAsync(int id, RoleDto statusdto);

        /// <summary>
        /// Supprime l'entrée role à l'identifiant spécifié
        /// </summary>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Contrôle si le role existe à ce nom
        /// </summary>
        Task<bool> ExistsByName(string name);
    }
}
