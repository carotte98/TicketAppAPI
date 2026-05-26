using Microsoft.AspNetCore.Mvc;
using TicketApp.Models.DTOs;
using TicketApp.Services.Interfaces;

namespace TicketApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;
        private readonly ILogger<StatusDto> _logger;

        public StatusController(IStatusService statusService, ILogger<StatusDto> logger)
        {
            _statusService = statusService;
            _logger = logger;
        }

        /// <summary>
        /// Renvoie tous les status
        /// </summary>
        /// <returns>OK avec Enumerable des statuts</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Récupération des statuts");

            var result = await _statusService.GetAllAsync();

            return Ok(result);
        }

        /// <summary>
        /// Renvoie un statut donné
        /// </summary>
        /// <param name="id">L'id du statut</param>
        /// <returns>NotFound si inexistant, OK avec l'objet si existant</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Récupération du statut à l'{id}", id);

            var status = await _statusService.GetByIdAsync(id);

            if (status == null)
                return NotFound();

            return Ok(status);
        }

        /// <summary>
        /// Crée un nouveau Statut
        /// </summary>
        /// <param name="dto">DTO avec le statut</param>
        /// <returns>CreatedAtAction avec objet si créé, Conflicts si doublon</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StatusDto dto)
        {
            if (await _statusService.ExistsByName(dto.nameStatus))
                return Conflict($"Un statut nommé '{dto.nameStatus}' existe déjà");

            var status = await _statusService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = status.idStatus }, status);
        }

        /// <summary>
        /// Mets à jour un statut
        /// </summary>
        /// <param name="id">l'id du statut</param>
        /// <param name="dto">DTO contenat les changements</param>
        /// <returns>NotFound si le statut n'existe pas, OK avec objet mis à jour sinon</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StatusDto dto)
        {
            var status = await _statusService.UpdateAsync(id, dto);

            if (status == null)
                return NotFound();

            return Ok(status);
        }

        /// <summary>
        /// Supprime un statut
        /// </summary>
        /// <param name="id">Id du statut</param>
        /// <returns>NotFound si inexistant, Nocontent si supprimé</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _statusService.DeleteAsync(id);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
