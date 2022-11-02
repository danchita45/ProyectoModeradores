using Microsoft.AspNetCore.Mvc;
using ProyectoModeradores.Models;
using ProyectoModeradores.Models.Connection;
using System.Data;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using NPOI.SS.Formula.PTG;

namespace ProyectoModeradores.Controllers
{

    public class ModeradoresController : Controller
    {
        List<Moderador> moderadors = new List<Moderador>();
        List<Area> areas = new List<Area>();
        

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
                    Area1 = lRow["Area1"] != DBNull.Value ? Convert.ToInt16(lRow["Area1"]) : Convert.ToInt16(0),
                    Area2 = lRow["Area2"] != DBNull.Value ? Convert.ToInt16(lRow["Area2"]) : Convert.ToInt16(0),
                    DArea1 = lRow["ADESC1"].ToString(),
                    DArea2 = lRow["ADESC2"].ToString(),
                    InstitucionId = lRow["InstitucionId"].ToString(),
                    statusId = Convert.ToInt32(lRow["StatusId"]),
                });

            }
            return View(moderadors);

            return View();
        }
        public IActionResult Index()
        {
            DataTable dataTable = ModeradorDB.ViewMods();


            foreach (DataRow lRow in dataTable.Rows)
            {
                moderadors.Add(new Moderador()
                {
                    Id = Convert.ToInt32(lRow["id_Moderador"]),
                    Name = lRow["Nombre"].ToString(),
                    Area1 = lRow["AreaId1"] != DBNull.Value ? Convert.ToInt16(lRow["AreaId1"]) : Convert.ToInt16(0),
                    Area2 = lRow["AreaId2"] != DBNull.Value ? Convert.ToInt16(lRow["AreaId2"]) : Convert.ToInt16(0),
                    DArea1 = lRow["ADESC1"].ToString(),
                    DArea2 = lRow["ADESC2"].ToString(),
                    InstitucionId = lRow["InstitucionId"].ToString(),
                    statusId = Convert.ToInt32(lRow["StatusId"]),
                });

            }
            GetCatalogs();
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
                    mod.Area1 = Convert.ToInt32(lRow["Area1"]);
                    mod.Area2 = Convert.ToInt32(lRow["Area2"]);
                    mod.DArea1 = lRow["ADESC1"].ToString();
                    mod.DArea2 = lRow["ADESC2"].ToString();
                    mod.InstitucionId = lRow["InstitucionId"].ToString();
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


        public void GetCatalogs()
        {

            DataTable dtAreas = AreaDB.ViewArea();

            foreach(DataRow lRow in dtAreas.Rows)
            {
                areas.Add(new Area()
                {
                    AreaId = Convert.ToInt32(lRow["AreaId"]),
                    Code = lRow["Code"].ToString(),
                    Description = lRow["Description"].ToString()
                });
            }

            ViewData["Areas"] = areas;

        }


    }
}
