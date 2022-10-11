using Microsoft.AspNetCore.Mvc;

namespace ProyectoModeradores.Controllers
{
    public class SalasController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
