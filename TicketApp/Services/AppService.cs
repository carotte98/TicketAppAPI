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

        public async Task<IEnumerable<AppDto>> GetAllAsync()
        {
            return await _context.Apps
                .AsNoTracking()
                .Select(p => MapToDto(p))
                .ToListAsync();
        }

        public async Task<AppDto?> GetByIdAsync(int id)
        {
            var app = await _context.Apps
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.id == id);

            return app == null ? null : MapToDto(app);
        }


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

        public async Task<bool> ExistsByName(string name)
        {
            return await _context.Apps.AnyAsync(p => p.name.ToLower() == name.ToLower());
        }


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
