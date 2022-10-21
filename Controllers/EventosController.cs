using Microsoft.AspNetCore.Mvc;

namespace ProyectoModeradores.Controllers
{
    public class EventosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
