using Microsoft.EntityFrameworkCore;
using TicketApp.Data;
using TicketApp.Models.DTOs;
using TicketApp.Models.Entities;
using TicketApp.Services.Interfaces;

namespace TicketApp.Services
{
    public class TicketTypeService : ITicketTypeService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TicketTypeService> _logger;

        public TicketTypeService(ApplicationDbContext context, ILogger<TicketTypeService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Renvoie TOUS les Types de Ticket
        /// </summary>
        /// <returns>Enumerable des Types de Tickets</returns>
        public async Task<IEnumerable<TicketTypeDto>> GetAllAsync()
        {
            return await _context.TicketTypes
                .AsNoTracking()
                .Select(p => MapToDto(p))
                .ToListAsync();
        }

        /// <summary>
        /// Renvoie le Type de Ticket avec l'id donné
        /// </summary>
        /// <param name="id">L'id du type</param>
        /// <returns>Le type à l'id</returns>
        public async Task<TicketTypeDto?> GetByIdAsync(int id)
        {
            var ticketType = await _context.TicketTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.id == id);

            return ticketType == null ? null : MapToDto(ticketType);
        }

        /// <summary>
        /// Crée un nouveau type de Ticket
        /// </summary>
        /// <param name="ticketTypeDto">La DTO avec le type de ticket</param>
        /// <returns>Le type de ticket une fois créé</returns>
        public async Task<TicketTypeDto> CreateAsync(TicketTypeDto ticketTypeDto)
        {
            var ticketType = new TicketType
            {
                name = ticketTypeDto.nameTicketType
            };

            _context.TicketTypes.Add(ticketType);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Type créé avec l'id {id}: {name}", ticketType.id, ticketType.name);

            return MapToDto(ticketType);
        }

        /// <summary>
        /// Mets à jour le type de ticket à l'id donnée
        /// </summary>
        /// <param name="id">l'id du type de ticket</param>
        /// <param name="ticketTypedto">La DTO avec les changements</param>
        /// <returns>Le type une fois mis à jour</returns>
        public async Task<TicketTypeDto?> UpdateAsync(int id, TicketTypeDto ticketTypedto)
        {
            var ticketType = await _context.TicketTypes.FindAsync(id);

            if (ticketType == null)
            {
                _logger.LogWarning("Tentative d'un Type inexistant : {id}", id);
                return null;
            }

            ticketType.name = ticketTypedto.nameTicketType;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Type mis à jour: {id}", id);

            return MapToDto(ticketType);
        }

        /// <summary>
        /// Supprime le type de ticket à l'id donné
        /// </summary>
        /// <param name="id">l'id du type de ticket</param>
        /// <returns>Vrai si supprimé, faux dans le cas contraire</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var ticketType = await _context.TicketTypes.FindAsync(id);

            if (ticketType == null)
            {
                return false;
            }

            _context.TicketTypes.Remove(ticketType);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Type supprimé: {id}", id);

            return true;
        }

        /// <summary>
        /// Contrôle si le type existe déjà
        /// </summary>
        /// <param name="name">Le nom du type</param>
        /// <returns>Vrai si existant, sinon faux</returns>
        public async Task<bool> ExistsByName(string name)
        {
            return await _context.TicketTypes.AnyAsync(p => p.name.ToLower() == name.ToLower());
        }

        /// <summary>
        /// Lie l'objet Type à sa DTO
        /// </summary>
        /// <param name="app">Le type</param>
        /// <returns>La DTO</returns>
        private static TicketTypeDto MapToDto(TicketType app)
        {
            return new TicketTypeDto
            {
                idTicketType = app.id,
                nameTicketType = app.name,
            };
        }
    }
}
