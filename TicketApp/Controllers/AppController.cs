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

        /// <summary>
        /// Renvoie toutes les Apps
        /// </summary>
        /// <returns>OK avec Enumerable des Apps</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Récupération des apps");

            var result = await _appService.GetAllAsync();

            return Ok(result);
        }

        /// <summary>
        /// Renvoie l'app à un id donné
        /// </summary>
        /// <param name="id">l'id de l'app</param>
        /// <returns>NotFound si inexistant, OK avec l'objet si existant</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Récupération de l'application à l'{id}", id);

            var app = await _appService.GetByIdAsync(id);

            if (app == null)
                return NotFound();

            return Ok(app);
        }

        /// <summary>
        /// Crée une nouvelle app
        /// </summary>
        /// <param name="dto">La DTO avec l'app</param>
        /// <returns>CreatedAtAction avec l'objet</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]AppDto dto)
        {
            if(await _appService.ExistsByName(dto.nameApp))
                return Conflict($"Une app nommé '{dto.nameApp}' existe déjà");

            var app = await _appService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = app.idApp }, app);
        }

        /// <summary>
        /// Mets à jour l'app
        /// </summary>
        /// <param name="id">l'id de l'app</param>
        /// <param name="dto">La DTO avec les changements</param>
        /// <returns>NotFound si l'app n'existe pas, OK si mis à jour</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AppDto dto) 
        {
            var app = await _appService.UpdateAsync(id, dto);

            if (app == null)
                return NotFound();

            return Ok(app);
        }

        /// <summary>
        /// Supprime une app
        /// </summary>
        /// <param name="id">l'id de l'app</param>
        /// <returns>NoContent si supprimé, sinon NotFound</returns>
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
