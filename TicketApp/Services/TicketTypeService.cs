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

        public async Task<IEnumerable<TicketTypeDto>> GetAllAsync()
        {
            return await _context.TicketTypes
                .AsNoTracking()
                .Select(p => MapToDto(p))
                .ToListAsync();
        }

        public async Task<TicketTypeDto?> GetByIdAsync(int id)
        {
            var ticketType = await _context.TicketTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.id == id);

            return ticketType == null ? null : MapToDto(ticketType);
        }


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

        public async Task<bool> ExistsByName(string name)
        {
            return await _context.TicketTypes.AnyAsync(p => p.name.ToLower() == name.ToLower());
        }


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
