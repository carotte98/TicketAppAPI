using Microsoft.EntityFrameworkCore;
using TicketApp.Data;
using TicketApp.Models.DTOs;
using TicketApp.Models.Entities;
using TicketApp.Services.Interfaces;


namespace TicketApp.Services
{
    public class StatusService : IStatusService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StatusService> _logger;

        public StatusService(ApplicationDbContext context, ILogger<StatusService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// renvoie tous les statuts
        /// </summary>
        /// <returns>Enumerable de tous les status</returns>
        public async Task<IEnumerable<StatusDto>> GetAllAsync()
        {
            return await _context.Statuses
                .AsNoTracking()
                .Select(p => MapToDto(p))
                .ToListAsync();
        }

        /// <summary>
        /// Renvoie le Ticket à un id donné
        /// </summary>
        /// <param name="id">L'id du Statut</param>
        /// <returns>Null si le Statut est inexistant, sinon le Statut</returns>
        public async Task<StatusDto?> GetByIdAsync(int id)
        {
            var status = await _context.Statuses
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.id == id);

            return status == null ? null : MapToDto(status);
        }

        /// <summary>
        /// Crée un nouveau statut
        /// </summary>
        /// <param name="statusDto">La DTO du statut à créer</param>
        /// <returns>Un DTO du statut une fois créé</returns>
        public async Task<StatusDto> CreateAsync(StatusDto statusDto)
        {
            var status = new Status
            {
                name = statusDto.nameStatus
            };

            _context.Statuses.Add(status);
            await _context.SaveChangesAsync();

            _logger.LogInformation("App créé avec l'id {id}: {name}", status.id, status.name);

            return MapToDto(status);
        }

        /// <summary>
        /// Mets à jour le ticket à l'id donné
        /// </summary>
        /// <param name="id">L'id du Ticket</param>
        /// <param name="statusdto">Le DTO avec les changements</param>
        /// <returns>Le Dto avec le statut changé</returns>
        public async Task<StatusDto?> UpdateAsync(int id, StatusDto statusdto)
        {
            var status = await _context.Statuses.FindAsync(id);

            if (status == null)
            {
                _logger.LogWarning("Tentative d'un statut inexistante : {id}", id);
                return null;
            }

            status.name = statusdto.nameStatus;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Statut mis à jour: {id}", id);

            return MapToDto(status);
        }

        /// <summary>
        /// Supprime le statut à l'id donné
        /// </summary>
        /// <param name="id">L'id du statut</param>
        /// <returns>Vrai si supprimé avec succès, sinon Faux</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var status = await _context.Statuses.FindAsync(id);

            if (status == null)
            {
                return false;
            }

            _context.Statuses.Remove(status);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Status supprimée: {id}", id);

            return true;
        }

        /// <summary>
        /// Contrôle si le Statut existe sous ce nom
        /// </summary>
        /// <param name="name">Le nom du statut</param>
        /// <returns>Renvoie faux si inexistant, vrai si déjà existant</returns>
        public async Task<bool> ExistsByName(string name)
        {
            return await _context.Statuses.AnyAsync(p => p.name.ToLower() == name.ToLower());
        }

        /// <summary>
        /// Lie Un statut à sa DTO
        /// </summary>
        /// <param name="status">L'objet statut à lier</param>
        /// <returns>Un statutDto du Statut</returns>
        private static StatusDto MapToDto(Status status)
        {
            return new StatusDto
            {
                idStatus = status.id,
                nameStatus = status.name,
            };
        }
    }
}
