using Microsoft.AspNetCore.Mvc;
using ProyectoModeradores.Models.Connection;
using ProyectoModeradores.Models;
using System.Data;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Connections.Features;

namespace ProyectoModeradores.Controllers
{
    public class EventosController : Controller
    {
        List<Evento> ev = new List<Evento>();
        List<Sala> salas = new List<Sala>();
        List<Area> area = new List<Area>();
        List<Status> statuses = new List<Status>();
        List<Moderador> moderadors= new List<Moderador>();
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
                    StatusId = Convert.ToInt16(lRow["StatusId"]),
                    ModeradorId= Convert.ToInt16(lRow["ModeradorId"]),
                    AreaId= Convert.ToInt16(lRow["AreaId"]),
                    SalaId= Convert.ToInt16(lRow["SalaId"]),

                    Status = new Status()
                    {
                        Description = lRow["SDESC"].ToString()
                    },

                    Moderador = new Moderador()
                    {
                        Name = lRow["MName"].ToString()
                    },

                    Area = new Area()
                    {
                        Description = lRow["ADESC"].ToString()
                    },

                    Sala = new Sala()
                    {
                        Description = lRow["SALADESC"].ToString()
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
            System.Data.DataTable DTB = SalasDB.ViewSala();
            foreach (DataRow lRow in DTB.Rows)
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
            DataTable DTC = ModeradorDB.ViewMods();


            foreach (DataRow lRow in DTC.Rows)
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
            ViewData["Areas"] = area;
            ViewData["Statuses"] = statuses;
            ViewData["Salas"] = salas;
            ViewData["Moderadores"] = moderadors;
            ViewData["Eventos"] = ev;
            return View();
        }
    
        public IActionResult Save(Evento E)
        {
            EventoDB.SaveEvent(E);
            if (E.EventId == null)
            {

            }

            return Redirect(nameof(Index));
        }
    
    
    }



}