using Microsoft.AspNetCore.Mvc;
using TicketApp.Models.DTOs;
using TicketApp.Services.Interfaces;

namespace TicketApp.Controllers
{
    /// <summary>
    ///     Classe AppController
    ///     
    ///     Gère les routes Http pour la Table App
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AppController : ControllerBase
    {
        private readonly IAppService _appService;
        private readonly ILogger<AppDto> _logger;

        public AppController(IAppService appService, ILogger<AppDto> logger)
        {
            _appService = appService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Récupération des apps");

            var result = await _appService.GetAllAsync();

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Récupération de l'application à l'{id}", id);

            var app = await _appService.GetByIdAsync(id);

            if (app == null)
                return NotFound();

            return Ok(app);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]AppDto dto)
        {
            if(await _appService.ExistsByName(dto.nameApp))
                return Conflict($"Une app nommé '{dto.nameApp}' existe déjà");

            var app = await _appService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = app.idApp }, app);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AppDto dto) 
        {
            var app = await _appService.UpdateAsync(id, dto);

            if (app == null)
                return NotFound();

            return Ok(app);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _appService.DeleteAsync(id);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
