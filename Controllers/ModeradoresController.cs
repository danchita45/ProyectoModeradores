using Microsoft.AspNetCore.Mvc;
using ProyectoModeradores.Models;

    

namespace ProyectoModeradores.Controllers
{
    
    public class ModeradoresController : Controller
    {
        List<Moderador> moderadors = new List<Moderador>();
        public IActionResult Index()
        {
            Moderador moderador = new Moderador();
            moderador.Id = 1;
            moderador.Name = "Ricardo";
            moderador.ApellidoP = "Barrera";
            moderador.ApellidoM = "Martinez";
            moderador.Area1 = new Area()
            {
                AreaId = 1,
                Code = "SC",
                Description = " ciencias"
            };
            moderador.Area2 = new Area()
            {
                AreaId = 1,
                Code = "SC",
                Description = " ciencias"
            };
            moderador.statusId = 1;
            moderador.InstitucionId = 1;

            moderadors.Add(moderador);

            return View(moderadors);
        }

       [HttpPost]
        public IActionResult Consultar(string User, string Pass)
        {
            return Redirect(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Import(IFormFile FileData)
        {
           
            return Redirect("/Moderadores/Index");
        }



    }
}
