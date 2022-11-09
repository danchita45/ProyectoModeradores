using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ProyectoModeradores.Models;
using ProyectoModeradores.Models.Connection;
using System.Data;

namespace ProyectoModeradores.Controllers
{
    public class SalasController : Controller
    {
        List<Sala> salas = new List<Sala>();
        List<Area> area = new List<Area>();
        List<Status> statuses = new List<Status>();
        List<Moderador> Mods = new List<Moderador>();
        public IActionResult Index()
        {
            System.Data.DataTable dataTable = SalasDB.ViewSala();


            foreach (DataRow lRow in dataTable.Rows)
            {
                salas.Add(new Sala()
                {
                    SalaId = Convert.ToInt32(lRow["SalaId"]),
                    Code = lRow["Code"].ToString(),
                    Description = lRow["Description"].ToString(),
                    Area = new Area()
                    {
                        Description = lRow["ADESC"].ToString()
                    },
                    Status = new Status()
                    {
                        StatusId = Convert.ToInt16(lRow["StatusId"]),
                        Description = lRow["SDESC"].ToString(),
                    }
                });

            }
            System.Data.DataTable dtA = AreaDB.ViewArea();
            foreach (DataRow lRow in dtA.Rows)
            {
                area.Add(new Area()
                {
                    AreaId = Convert.ToUInt16(lRow["AreaId"]),
                    Code = lRow["Code"].ToString(),
                    Description = lRow["Description"].ToString(),
                });

            }
            System.Data.DataTable dtS = StatusDB.ViewStatus();
            foreach (DataRow lRow in dtS.Rows)
            {
                statuses.Add(new Status()
                {
                    StatusId = Convert.ToUInt16(lRow["StatusId"]),
                    Code = lRow["Code"].ToString(),
                    Description = lRow["Description"].ToString(),
                });

            }
            System.Data.DataTable dtM = ModeradorDB.ViewMods();
            foreach (DataRow lRow in dtM.Rows)
            {
                Mods.Add(new Moderador()
                {
                    Id = Convert.ToUInt16(lRow["id_Moderador"]),
                    Name = lRow["Nombre"].ToString(),
                });

            }
            ViewData["Areas"] = area;
            ViewData["Statuses"] = statuses;
            ViewData["Salas"] = salas;
            ViewData["Moderadors"] = Mods;
            return View();
        }

        public IActionResult Save(Sala sala)
        {
            if (sala.SalaId != 0)
            {
                SalasDB.UpdateSala(sala);
            }
            SalasDB.SaveSala(sala);


            return Redirect(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            SalasDB.DeleteSala(id);
            return Redirect("/Salas/Index");
        }



        [HttpPost]
        public IActionResult Import(IFormFile FileData)
        {

            Stream stream = FileData.OpenReadStream();
            IWorkbook Excel = null;
            if (Path.GetExtension(FileData.FileName) == ".xlsx")
            {
                Excel = new XSSFWorkbook(stream);
            }

            ISheet Hoja = Excel.GetSheetAt(0);
            int cantidadFilas = Hoja.LastRowNum;
            for (int i = 1; i < cantidadFilas; i++)
            {
                IRow fila = Hoja.GetRow(i);
                salas.Add(new Sala()
                {
                    Code= fila.GetCell(1).ToString(),
                    Fecha = fila.GetCell(11).ToString(),
                    AreaD = fila.GetCell(2).ToString(),
                    Bloque = fila.GetCell(14).ToString(),
                    Salon = fila.GetCell(15).ToString(),
                    Ubicacion = fila.GetCell(16).ToString()
                });
            }


            foreach (Sala S in salas)
            {

                string[] subs = S.AreaD.Split(':');


                S.AreaId = SalasDB.serchArea(subs[0],S.Code);
                if (S.AreaId != 0)
                {
                    SalasDB.SaveSala(S);
                }

                
            }
            return Redirect("/");

        }
    }
}
