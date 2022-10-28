using Microsoft.AspNetCore.Mvc;
using ProyectoModeradores.Models;
using ProyectoModeradores.Models.Connection;
using System.Data;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

namespace ProyectoModeradores.Controllers
{

    public class ModeradoresController : Controller
    {
        List<Moderador> moderadors = new List<Moderador>();


        [HttpPost]
        public IActionResult Index(string Nombre)
        {
            DataTable dt = new DataTable();
            dt= ModeradorDB.SelectModByName(Nombre);
            foreach (DataRow lRow in dt.Rows)
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

            return View();
        }
        public IActionResult Index( )
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
        public IActionResult Import(IFormFile FileData)
        {
            List<Moderador> Mod = new List<Moderador>();
            Stream stream = FileData.OpenReadStream();
            IWorkbook Excel = null;
            if (Path.GetExtension(FileData.FileName) == ".xlsx")
            {
                Excel = new XSSFWorkbook(stream);
            }


            ISheet Hoja = Excel.GetSheetAt(0);
            int CantidadFilas = Hoja.LastRowNum;
            for (int i = 1; i <= CantidadFilas; i++)
            {
                IRow fila = Hoja.GetRow(i);
                Mod.Add(new Moderador()
                {
                        
                        Name = fila.GetCell(6).ToString(),
                        email = fila.GetCell(8).ToString(),
                        InstitucionId= fila.GetCell(2).ToString()



                }); ;

            }
            foreach(Moderador M in Mod)
            {
                ModeradorDB.SaveMod(M);
            }


            return Redirect("/");
        }



    }
}
