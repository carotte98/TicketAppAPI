using Microsoft.AspNetCore.Mvc;
using TicketApp.Models.DTOs;
using TicketApp.Services.Interfaces;

namespace TicketApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly ILogger<TicketController> _logger;

        public TicketController(ITicketService ticketService, ILogger<TicketController> logger)
        {
            _ticketService = ticketService;
            _logger = logger;
        }

        /// <summary>
        /// Renvoie tous les Tickets
        /// </summary>
        /// <returns>OK avec Enumerable de Tickets</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Récupération des tickets");

            var result = await _ticketService.GetAllAsync();

            return Ok(result);
        }

        /// <summary>
        /// Renvoie tous les tickets d'un dev
        /// </summary>
        /// <param name="name">Nom du Dev</param>
        /// <returns>Ok avec Enumerable de Tickets</returns>
        [HttpGet("/dev/{name}")]
        public async Task<IActionResult> GetByDevName(string name)
        {
            _logger.LogInformation("Récupération des tickets du Dev");

            var result = await _ticketService.GetByDevNameAsync(name);

            return Ok(result);
        }

        /// <summary>
        /// Renvoie tous les tickets d'un auteur
        /// </summary>
        /// <param name="name">Nom de l'auteur</param>
        /// <returns>OK avec Enumerable des Tickets</returns>
        [HttpGet("/author/{name}")]
        public async Task<IActionResult> GetByAuthorName(string name)
        {
            _logger.LogInformation("Récupération des tickets de l'auteur");

            var result = await _ticketService.GetByAuthorNameAsync(name);

            return Ok(result);
        }

        /// <summary>
        /// Renvoie un ticket donné
        /// </summary>
        /// <param name="id">Id du Ticket</param>
        /// <returns>NotFound si inexistant, OK avec le Ticket sinon</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Récupération du ticket à l'{id}", id);

            var ticket = await _ticketService.GetByIdAsync(id);

            if (ticket == null)
                return NotFound();

            return Ok(ticket);
        }

        /// <summary>
        /// Crée un nouveau Ticket
        /// </summary>
        /// <param name="dto">DTO du Ticket</param>
        /// <returns>CreatedAtAction avec le ticket créé</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketDto dto)
        {
            var ticket = await _ticketService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = ticket.idTicket }, ticket);
        }

        /// <summary>
        /// Mets à jour un Ticket
        /// </summary>
        /// <param name="id">Id du Ticket</param>
        /// <param name="dto">DTO avec les changements</param>
        /// <returns>NotFound si inexistant, sinon OK avec Objet à jour</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTicketDto dto)
        {
            var ticket = await _ticketService.UpdateAsync(id, dto);

            if (ticket == null)
                return NotFound();

            return Ok(ticket);
        }

        /// <summary>
        /// Supprime un ticket
        /// </summary>
        /// <param name="id">ID du Ticket</param>
        /// <returns>NotFound si inexistant, sinon NoContent si supprimé</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _ticketService.DeleteAsync(id);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
