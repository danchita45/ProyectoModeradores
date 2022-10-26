using Microsoft.AspNetCore.Mvc;
using ProyectoModeradores.Models.Connection;
using ProyectoModeradores.Models;
using System.Data;
using Microsoft.Extensions.Logging;

namespace ProyectoModeradores.Controllers
{
    public class EventosController : Controller
    {
        List<Evento> ev = new List<Evento>();
        public IActionResult Index()
        {
            DataTable dataTable = EventoDB.ViewEvents();

            foreach (DataRow lRow in dataTable.Rows)
            {
                ev.Add(new Evento()
                {
                    EventId = Convert.ToInt32(lRow["EventId"]),
                    Name = lRow["Name"].ToString(),
                    Code = lRow["Code"].ToString(),
                    Description = lRow["Description"].ToString(),
                    FInicio = Convert.ToDateTime(lRow["FInicio"]),
                    FTermino = Convert.ToDateTime(lRow["FTermino"]),
                    Status = new Status()
                    {
                        Description = lRow["SDes"].ToString()
                    },

                    Moderador = new Moderador()
                    {
                        Name = lRow["MName"].ToString()
                    },

                    Area = new Area()
                    {
                        Description = lRow["ADescription"].ToString()
                    },

                    Sala = new Sala()
                    {
                        Description = lRow["SDescription"].ToString()
                    }

                });

            }
            ViewData["Eventos"] = ev;
            return View();
        }
    }
}