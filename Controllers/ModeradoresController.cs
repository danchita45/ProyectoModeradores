using Microsoft.AspNetCore.Mvc;

namespace ProyectoModeradores.Controllers
{
    public class ModeradoresController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

       [HttpPost]
        public IActionResult Consultar(string User, string Pass)
        {
            return Redirect(nameof(Index));
        }
    }
}
