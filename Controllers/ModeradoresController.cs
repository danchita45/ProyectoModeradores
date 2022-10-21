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
            if (mod.Id == null)
            {
                ModeradorDB.SaveMod(mod);
            }
            ModeradorDB.UpdateMod(mod);
            return Redirect(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            ModeradorDB.DeleteMod(id);
            return Redirect("/Moderadores/Index");
        }

        public IActionResult Edit(int? id)
        {
            DataTable dataTable = ModeradorDB.SelectMod((int)id);

            Moderador mod = new Moderador();
            foreach (DataRow lRow in dataTable.Rows)
            {



                mod.Id = Convert.ToInt32(lRow["id_Moderador"]);
                mod.Name = lRow["Nombre"].ToString();
                mod.ApellidoP = lRow["ApellidoP"].ToString();
                mod.ApellidoM = lRow["ApellidoM"].ToString();
                mod.statusId = Convert.ToInt32(lRow["StatusId"]);

            }

            return View(mod);
        }


        [HttpPost]
        public async Task<IActionResult> Import(IFormFile FileData)
        {

            return Redirect("/Moderadores/Index");
        }



    }
}
