using Microsoft.EntityFrameworkCore;
using TicketApp.Data;
using TicketApp.Models.DTOs;
using TicketApp.Models.Entities;
using TicketApp.Services.Interfaces;

namespace TicketApp.Services
{
    public class AppService : IAppService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AppService> _logger;

        public AppService(ApplicationDbContext context, ILogger<AppService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Renvoie toutes les Apps
        /// </summary>
        /// <returns>Enumerable des Apps</returns>
        public async Task<IEnumerable<AppDto>> GetAllAsync()
        {
            return await _context.Apps
                .AsNoTracking()
                .Select(p => MapToDto(p))
                .ToListAsync();
        }

        /// <summary>
        /// Renvoie l'App à l'id demandé
        /// </summary>
        /// <param name="id">L'id de l'app</param>
        /// <returns>L'app à l'id</returns>
        public async Task<AppDto?> GetByIdAsync(int id)
        {
            var app = await _context.Apps
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.id == id);

            return app == null ? null : MapToDto(app);
        }

        /// <summary>
        /// Crée une nouvelle app
        /// </summary>
        /// <param name="appDto">La DTO avec la nouvelle app</param>
        /// <returns>Une DTO de l'app nouvellement créée</returns>
        public async Task<AppDto> CreateAsync(AppDto appDto)
        {
            var app = new App
            {
                name = appDto.nameApp
            };

            _context.Apps.Add(app);
            await _context.SaveChangesAsync();

            _logger.LogInformation("App créé avec l'id {id}: {name}", app.id, app.name);

            return MapToDto(app);
        }

        /// <summary>
        /// Mets à jour l'app à l'id indiqué
        /// </summary>
        /// <param name="id">L'id de l'app</param>
        /// <param name="appdto">La DTO avec les changements</param>
        /// <returns>Une DTO de l'app à jour</returns>
        public async Task<AppDto?> UpdateAsync(int id, AppDto appdto)
        {
            var app = await _context.Apps.FindAsync(id);

            if (app == null) 
            {
                _logger.LogWarning("Tentative d'une app inexistante : {id}", id);
                return null;
            }

            app.name = appdto.nameApp;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Produit mis à jour: {id}", id);

            return MapToDto(app);
        }

        /// <summary>
        /// Supprime l'app à l'id indiqué
        /// </summary>
        /// <param name="id">L'id de l'app</param>
        /// <returns>Vrai si supprimé, faux dans le cas contraire</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var app = await _context.Apps.FindAsync(id);

            if (app == null)
            { 
                return false;
            }

            _context.Apps.Remove(app);
            await _context.SaveChangesAsync();

            _logger.LogInformation("App supprimée: {id}", id);

            return true;
        }

        /// <summary>
        /// Contrôle si l'app existe à ce nom
        /// </summary>
        /// <param name="name">Le nom de l'app</param>
        /// <returns>Vrai si déjà existant, faux sinon</returns>
        public async Task<bool> ExistsByName(string name)
        {
            return await _context.Apps.AnyAsync(p => p.name.ToLower() == name.ToLower());
        }

        /// <summary>
        /// Lie un Objet App à sa DTO
        /// </summary>
        /// <param name="app">L'app à lier</param>
        /// <returns>La Dto de l'app</returns>
        private static AppDto MapToDto(App app)
        {
            return new AppDto
            {
                idApp = app.id,
                nameApp = app.name,
            };
        }



    }
}
