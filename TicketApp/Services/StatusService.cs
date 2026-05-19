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

        public async Task<IEnumerable<StatusDto>> GetAllAsync()
        {
            return await _context.Statuses
                .AsNoTracking()
                .Select(p => MapToDto(p))
                .ToListAsync();
        }

        public async Task<StatusDto?> GetByIdAsync(int id)
        {
            var status = await _context.Statuses
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.id == id);

            return status == null ? null : MapToDto(status);
        }


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

        public async Task<bool> ExistsByName(string name)
        {
            return await _context.Statuses.AnyAsync(p => p.name.ToLower() == name.ToLower());
        }


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
