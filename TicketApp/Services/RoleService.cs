using Microsoft.EntityFrameworkCore;
using TicketApp.Data;
using TicketApp.Models.DTOs;
using TicketApp.Models.Entities;
using TicketApp.Services.Interfaces;

namespace TicketApp.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RoleService> _logger;

        public RoleService(ApplicationDbContext context, ILogger<RoleService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// renvoie tous les roles
        /// </summary>
        /// <returns>Enumerable de tous les roles</returns>
        public async Task<IEnumerable<RoleDto>> GetAllAsync()
        {
            return await _context.Roles
                .AsNoTracking()
                .Select(p => MapToDto(p))
                .ToListAsync();
        }

        /// <summary>
        /// Renvoie le Role à un id donné
        /// </summary>
        /// <param name="id">L'id du Role</param>
        /// <returns>Null si le Role est inexistant, sinon le role</returns>
        public async Task<RoleDto?> GetByIdAsync(int id)
        {
            var role = await _context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.id == id);

            return role == null ? null : MapToDto(role);
        }

        /// <summary>
        /// Crée un nouveau role
        /// </summary>
        /// <param name="roleDto">La DTO du role à créer</param>
        /// <returns>Un DTO du role une fois créé</returns>
        public async Task<RoleDto> CreateAsync(RoleDto roleDto)
        {
            var role = new Role
            {
                name = roleDto.nameRole
            };

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Role créé avec l'id {id}: {name}", role.id, role.name);

            return MapToDto(role);
        }

        /// <summary>
        /// Mets à jour le role à l'id donné
        /// </summary>
        /// <param name="id">L'id du Role</param>
        /// <param name="roledto">Le DTO avec les changements</param>
        /// <returns>Le Dto avec le role changé</returns>
        public async Task<RoleDto?> UpdateAsync(int id, RoleDto roledto)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                _logger.LogWarning("Tentative d'un role inexistante : {id}", id);
                return null;
            }

            role.name = roledto.nameRole;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Role mis à jour: {id}", id);

            return MapToDto(role);
        }

        /// <summary>
        /// Supprime le role à l'id donné
        /// </summary>
        /// <param name="id">L'id du role</param>
        /// <returns>Vrai si supprimé avec succès, sinon Faux</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return false;
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Role supprimée: {id}", id);

            return true;
        }

        /// <summary>
        /// Contrôle si le Role existe sous ce nom
        /// </summary>
        /// <param name="name">Le nom du role</param>
        /// <returns>Renvoie faux si inexistant, vrai si déjà existant</returns>
        public async Task<bool> ExistsByName(string name)
        {
            return await _context.Roles.AnyAsync(p => p.name.ToLower() == name.ToLower());
        }

        /// <summary>
        /// Lie Un role à sa DTO
        /// </summary>
        /// <param name="role">L'objet role à lier</param>
        /// <returns>Un statutDto du Statut</returns>
        private static RoleDto MapToDto(Role role)
        {
            return new RoleDto
            {
                idRole = role.id,
                nameRole = role.name,
            };
        }
    }
}
