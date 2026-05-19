using Microsoft.AspNetCore.Mvc;

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
        /// <summary>
        ///     Méthode GetAll
        ///     
        ///     Renvoie toutes les applications dans la table App
        /// </summary>
        /// <returns>Un JSON avec les Apps</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            return null;
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return null;
        }
    }
}
