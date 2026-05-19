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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Récupération des statuts");

            var result = await _statusService.GetAllAsync();

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Récupération du statut à l'{id}", id);

            var status = await _statusService.GetByIdAsync(id);

            if (status == null)
                return NotFound();

            return Ok(status);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StatusDto dto)
        {
            if (await _statusService.ExistsByName(dto.nameStatus))
                return Conflict($"Un statut nommé '{dto.nameStatus}' existe déjà");

            var status = await _statusService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = status.idStatus }, status);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StatusDto dto)
        {
            var status = await _statusService.UpdateAsync(id, dto);

            if (status == null)
                return NotFound();

            return Ok(status);
        }

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
