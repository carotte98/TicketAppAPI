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

        /// <summary>
        /// Renvoie TOUS les Tickets
        /// </summary>
        /// <returns>Enumerable avec tout les Tickets</returns>
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

        /// <summary>
        /// Renvoie un Ticket avec un id demandé
        /// </summary>
        /// <param name="id">L'id du Ticket demandé</param>
        /// <returns>Null si le Ticket n'existe pas, sinon le Ticket</returns>
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

        /// <summary>
        /// Renvoie tous les Tickets liée à un dev en particulier
        /// </summary>
        /// <param name="name">Le nom du dev</param>
        /// <returns>Enumerable de tout les Tickets du dev</returns>
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

        /// <summary>
        /// Trouve et renvoie tout les tickets d'un autheur demandé
        /// </summary>
        /// <param name="name">Le nom de l'auteur</param>
        /// <returns>Enumerable des Tickets de l'auteur</returns>
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

        /// <summary>
        /// Crée un nouveau Ticket
        /// </summary>
        /// <param name="ticketDto">La DTO avec les information du nouveau Ticket</param>
        /// <returns>Un GetDTO avec le nouveau Ticket</returns>
        public async Task<GetTicketDto> CreateAsync(CreateTicketDto ticketDto)
        {   
            // Création d'un nouveau ticket à partir des données en DTO Create
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

        /// <summary>
        /// Mets à jour un Ticket à l'id indiqué
        /// </summary>
        /// <param name="id">L'id du Ticket à mettre à jour</param>
        /// <param name="ticketdto">La DTO avec les données à changer</param>
        /// <returns>Un GetDTO du Ticket mis à jour</returns>
        public async Task<GetTicketDto?> UpdateAsync(int id, UpdateTicketDto ticketdto)
        {
            var ticket = await _context.Tickets
                .Include(p => p.status)
                .Include(p => p.ticketType)
                .Include(p => p.app)
                .FirstOrDefaultAsync(p => p.id == id);

            // En cas de Ticket inexistant à l'id demandé, renvoie null
            if (ticket == null)
            {
                _logger.LogWarning("Tentative d'un ticket inexistante : {id}", id);
                return null;
            }

            // Assignation des changements au Ticket
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

        /// <summary>
        /// Supprime un Ticket à l'id fourni
        /// </summary>
        /// <param name="id">L'id du Ticket à supprimer</param>
        /// <returns>Un boolean, vrai si supprimé avec succès, faux en cas d'échec</returns>
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


        /// <summary>
        /// Map un Ticket fourni à la DTO 
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns>Le ticket sous forme de GetTicketDTO</returns>
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

    }
}
