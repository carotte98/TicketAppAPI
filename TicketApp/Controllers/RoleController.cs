using Microsoft.AspNetCore.Mvc;
using TicketApp.Models.DTOs;
using TicketApp.Services.Interfaces;

namespace TicketApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {

        private readonly IRoleService _roleService;
        private readonly ILogger<RoleDto> _logger;

        public RoleController(IRoleService roleService, ILogger<RoleDto> logger)
        {
            _statusService = roleService;
            _logger = logger;
        }

        /// <summary>
        /// Renvoie tous les roles
        /// </summary>
        /// <returns>OK avec Enumerable des roles</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Récupération des roles");

            var result = await _roleService.GetAllAsync();

            return Ok(result);
        }

        /// <summary>
        /// Renvoie un role donné
        /// </summary>
        /// <param name="id">L'id du role</param>
        /// <returns>NotFound si inexistant, OK avec l'objet si existant</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Récupération du role à l'{id}", id);

            var role = await _roleService.GetByIdAsync(id);

            if (role == null)
                return NotFound();

            return Ok(role);
        }

        /// <summary>
        /// Crée un nouveau Role
        /// </summary>
        /// <param name="dto">DTO avec le role</param>
        /// <returns>CreatedAtAction avec objet si créé, Conflicts si doublon</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleDto dto)
        {
            if (await _roleService.ExistsByName(dto.nameRole))
                return Conflict($"Un role nommé '{dto.nameRole}' existe déjà");

            var role = await _roleService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = role.idRole }, role);
        }

        /// <summary>
        /// Mets à jour un role
        /// </summary>
        /// <param name="id">l'id du role</param>
        /// <param name="dto">DTO contenat les changements</param>
        /// <returns>NotFound si le role n'existe pas, OK avec objet mis à jour sinon</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RoleDto dto)
        {
            var role = await _roleService.UpdateAsync(id, dto);

            if (role == null)
                return NotFound();

            return Ok(role);
        }
        
        /// <summary>
        /// Supprime un role
        /// </summary>
        /// <param name="id">Id du role</param>
        /// <returns>NotFound si inexistant, Nocontent si supprimé</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _roleService.DeleteAsync(id);

            if (!success)
                return NotFound();

            return NoContent();
        }

    }
}
