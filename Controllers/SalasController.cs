using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            System.Data.DataTable dataTable =SalasDB.ViewSala();


            foreach (DataRow lRow in dataTable.Rows)
            {
                salas.Add(new Sala()
                {
                    SalaId = Convert.ToInt32(lRow["SalaId"]),
                    Code = lRow["Code"].ToString(),
                    Description = lRow["Description"].ToString(),
                    Area = new Area()
                    {
                        Description= lRow["ADESC"].ToString()
                    },
                    Status= new Status()
                    {
                        StatusId= Convert.ToInt16(lRow["StatusId"]),
                        Description = lRow["SDESC"].ToString(),
                    }
                });

            }
            System.Data.DataTable dtA = AreaDB.ViewArea();
            foreach (DataRow lRow in dtA.Rows)
            {
                area.Add(new Area()
                {
                    AreaId= Convert.ToUInt16(lRow["AreaId"]),
                    Code= lRow["Code"].ToString(),
                    Description= lRow["Description"].ToString(),
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
            ViewData["Areas"] = area;
            ViewData["Statuses"] = statuses;
            ViewData["Salas"] = salas;
            return View();
        }

        public IActionResult Save(Sala sala)
        {
            if (!SalasDB.SaveSala(sala))
            {
                
            }
            return Redirect(nameof(Index));
        }
    }
}
