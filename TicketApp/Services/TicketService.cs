using Microsoft.EntityFrameworkCore;
using TicketApp.Data;
using TicketApp.Models.DTOs;
using TicketApp.Models.Entities;
using TicketApp.Services.Interfaces;

namespace TicketApp.Services
{
    public class TicketService :ITicketService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TicketService> _logger;

        public TicketService(ApplicationDbContext context, ILogger<TicketService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<GetTicketDto>> GetAllAsync()
        {
            return await _context.Tickets
                .AsNoTracking()
                .Include(p => p.status)
                .Include(p => p.ticketType)
                .Include(p => p.app)
                .Select(p => MapToGetDto(p))
                .ToListAsync();
        }

        public async Task<GetTicketDto?> GetByIdAsync(int id)
        {
            var ticket = await _context.Tickets
                .AsNoTracking()
                .Include(p => p.status)
                .Include(p => p.ticketType)
                .Include(p => p.app)
                .FirstOrDefaultAsync(p => p.id == id);

            return ticket == null ? null : MapToGetDto(ticket);
        }

        //GET BY DEV NAME
        public async Task<IEnumerable<GetTicketDto>> GetByDevNameAsync(string name)
        {
            return await _context.Tickets
                .AsNoTracking()
                .Where(p => p.devName == name)
                .Include(p => p.status)
                .Include(p => p.ticketType)
                .Include(p => p.app)
                .Select(p => MapToGetDto(p))
                .ToListAsync();

        }

        //GET BY User NAME
        public async Task<IEnumerable<GetTicketDto>> GetByAuthorNameAsync(string name)
        {
            return await _context.Tickets
                .AsNoTracking()
                .Where(p => p.authorName == name)
                .Include(p => p.status)
                .Include(p => p.ticketType)
                .Include(p => p.app)
                .Select(p => MapToGetDto(p))
                .ToListAsync();

        }

        public async Task<GetTicketDto> CreateAsync(CreateTicketDto ticketDto)
        {
            var ticket = new Ticket
            {
                name = ticketDto.nameTicket,
                authorName = ticketDto.authorTicket,
                authorMsg = ticketDto.authorMsgTicket,
                startDate = ticketDto.startdateTicket,
                updateDate = ticketDto.updateDateTicket,
                idStatus = ticketDto.statusTicket,
                idTicketType = ticketDto.typeTicket,
                idApp = ticketDto.appTicket,
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Ticket créé avec l'id {id}: {name}", ticket.id, ticket.name);

            return MapToGetDto(ticket);
        }


        public async Task<GetTicketDto?> UpdateAsync(int id, UpdateTicketDto ticketdto)
        {
            var ticket = await _context.Tickets
                .Include(p => p.status)
                .Include(p => p.ticketType)
                .Include(p => p.app)
                .FirstOrDefaultAsync(p => p.id == id);

            if (ticket == null)
            {
                _logger.LogWarning("Tentative d'un ticket inexistante : {id}", id);
                return null;
            }

            ticket.authorMsg = ticketdto.authorMsgTicket;
            ticket.devName = ticketdto.devTicket;
            ticket.devMsg = ticketdto.devMsgTicket;
            ticket.updateDate = ticketdto.updateDateTicket;
            ticket.idStatus = ticketdto.idStatusTicket;
            ticket.idTicketType = ticketdto.idTypeTicket;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Ticket mis à jour: {id}", id);

            return MapToGetDto(ticket);
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return false;
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Ticket supprimée: {id}", id);

            return true;
        }



        private static GetTicketDto MapToGetDto(Ticket ticket)
        {
            return new GetTicketDto
            {
                idTicket = ticket.id,
                nameTicket = ticket.name,
                authorTicket = ticket.authorName,
                authorMsgTicket = ticket.authorMsg,
                devTicket = ticket.devName,
                devMsgTicket = ticket.devMsg,
                startdateTicket = ticket.startDate,
                updateDateTicket = ticket.updateDate,
                statusTicket = ticket.status,
                typeTicket = ticket.ticketType,
                appTicket = ticket.app,
            };
        }

        private static UpdateTicketDto MapToUpdateDto(Ticket ticket)
        {
            return new UpdateTicketDto
            {
                authorMsgTicket = ticket.authorMsg,
                devTicket = ticket.devName,
                devMsgTicket = ticket.devMsg,
                updateDateTicket = ticket.updateDate,
                idStatusTicket = ticket.idStatus,
                idTypeTicket = ticket.idTicketType,
            };
        }

        private static CreateTicketDto MapToCreateDto(Ticket ticket)
        {
            return new CreateTicketDto
            {
                nameTicket = ticket.name,
                authorTicket = ticket.authorName,
                authorMsgTicket = ticket.authorMsg,
                startdateTicket = ticket.startDate,
                updateDateTicket = ticket.updateDate,
                statusTicket = ticket.idStatus,
                typeTicket = ticket.idTicketType,
                appTicket = ticket.idApp,
            };
        }
    }
}
