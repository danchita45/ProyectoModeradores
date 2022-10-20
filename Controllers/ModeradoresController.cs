using Microsoft.AspNetCore.Mvc;
using ProyectoModeradores.Models;
using ProyectoModeradores.Models.Connection;
using System.Data;

namespace ProyectoModeradores.Controllers
{

    public class ModeradoresController : Controller
    {
        List<Moderador> moderadors = new List<Moderador>();
        public IActionResult Index()
        {
            DataTable dataTable = ModeradorDB.ViewMods();
           

            foreach (DataRow lRow in dataTable.Rows)
            {
                moderadors.Add(new Moderador()
                {
                    Id = Convert.ToInt32(lRow["id_Moderador"]),
                    Name = lRow["Nombre"].ToString(),
                    ApellidoP = lRow["ApellidoP"].ToString(),
                    ApellidoM = lRow["ApellidoM"].ToString(),
                    statusId = Convert.ToInt32(lRow["StatusId"]),
                });
                
            }
            return View(moderadors);
        }

        [HttpPost]
        public IActionResult Consultar(string User, string Pass)
        {
            return Redirect(nameof(Index));
        }

        [HttpPost]
        public IActionResult AgregarMod(Moderador mod)
        {
            if (ModeradorDB.SaveMod(mod))
            {

            }
            return Redirect(nameof(Index));
        }



        [HttpPost]
        public async Task<IActionResult> Import(IFormFile FileData)
        {

            return Redirect("/Moderadores/Index");
        }



    }
}
