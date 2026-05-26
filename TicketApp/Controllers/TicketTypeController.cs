using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TicketApp.Models.DTOs;
using TicketApp.Services.Interfaces;

namespace TicketApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketTypeController : ControllerBase
    {
        private readonly ITicketTypeService _ticketTypeService;
        private readonly ILogger<TicketTypeDto> _logger;

        public TicketTypeController(ITicketTypeService ticketTypeService, ILogger<TicketTypeDto> logger)
        {
            _ticketTypeService = ticketTypeService;
            _logger = logger;
        }

        /// <summary>
        /// Renvoie tous les types
        /// </summary>
        /// <returns>OK avec Enumerable des Types</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Récupération des types");

            var result = await _ticketTypeService.GetAllAsync();

            return Ok(result);
        }

        /// <summary>
        /// Renvoie un Type donné
        /// </summary>
        /// <param name="id">Id du Type</param>
        /// <returns>NotFound si inexistant, sinon OK avec le Type</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Récupération du Type à l'{id}", id);

            var ticketType = await _ticketTypeService.GetByIdAsync(id);

            if (ticketType == null)
                return NotFound();

            return Ok(ticketType);
        }

        /// <summary>
        /// Crée un Type
        /// </summary>
        /// <param name="dto">DTO avec le type</param>
        /// <returns>Conflicts en cas de doublon, CreatedAtAction avec l'objet sinon</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TicketTypeDto dto)
        {
            if (await _ticketTypeService.ExistsByName(dto.nameTicketType))
                return Conflict($"Une type nommé '{dto.nameTicketType}' existe déjà");

            var ticketType = await _ticketTypeService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = ticketType.idTicketType }, ticketType);
        }

        /// <summary>
        /// Mets à jour un Type
        /// </summary>
        /// <param name="id">Id du Type</param>
        /// <param name="dto">DTO avec les changements</param>
        /// <returns>NotFound si inexistant, OK avec objet mis à jour autrement</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TicketTypeDto dto)
        {
            var ticketType = await _ticketTypeService.UpdateAsync(id, dto);

            if (ticketType == null)
                return NotFound();

            return Ok(ticketType);
        }

        /// <summary>
        /// Supprime un Type
        /// </summary>
        /// <param name="id">Id du Type</param>
        /// <returns>NotFound is inexistant, NoContent si supprimé</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _ticketTypeService.DeleteAsync(id);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
