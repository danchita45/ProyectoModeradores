using Microsoft.AspNetCore.Mvc;
using ProyectoModeradores.Models.Connection;
using ProyectoModeradores.Models;
using System.Data;
using System.Net.Mail;

namespace ProyectoModeradores.Controllers
{
    public class SincroniaController : Controller
    {
        List<Area> areas = new List<Area>();
        List<Moderador> Mods = new List<Moderador>();
        List<Sala> Salas = new List<Sala>();

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Sincronizar()
        {
            bool res;
            int a = 0;
            DataTable dtM = ModeradorDB.ViewMods();
            foreach(DataRow lRow in dtM.Rows)
            {

                Mods.Add(new Moderador()
                {
                    Id = Convert.ToInt32(lRow["id_Moderador"]),
                    Area1 = lRow["AreaId1"] != DBNull.Value ? Convert.ToInt32(lRow["AreaId1"]) : Convert.ToInt32(0),
                    Area2 = lRow["AreaId2"] != DBNull.Value ? Convert.ToInt32(lRow["AreaId2"]) : Convert.ToInt32(0),
                });
            }
            DataTable dtS = SalasDB.ViewSala();
            foreach(DataRow lRow in dtS.Rows)
            {
                Salas.Add(new Sala()
                {
                    SalaId = Convert.ToInt32(lRow["SalaId"]),
                    AreaId = Convert.ToInt32(lRow["AreaId"]),
                });
            }
            foreach (  Sala s in Salas)
            {
                foreach(Moderador M in Mods)
                {
                    if (s.AreaId == M.Area1)
                    {
                        SincroniaDB.SaveData(s.SalaId,M.Id);
                    }
                    break;
                }
            }




            return RedirectToAction("Index");

        }
        public void GetCatalogs()
        {

            DataTable dtAreas = AreaDB.ViewArea();

            foreach (DataRow lRow in dtAreas.Rows)
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

        public IActionResult UpdateData(int Id)
        {
            List<Moderador> moders = new List<Moderador>();
            DataTable dataTable = ModeradorDB.SelectMod(Id);

            Moderador mod = new Moderador();
            foreach (DataRow lRow in dataTable.Rows)
            {
                mod.Id = Convert.ToInt32(lRow["id_Moderador"]);
                mod.Name = lRow["Nombre"].ToString();
                mod.Area1 = Convert.ToInt32(lRow["AreaId1"]);
                mod.Area2 = Convert.ToInt32(lRow["AreaId2"]);
                mod.DArea1 = lRow["ADESC1"].ToString();
                mod.DArea2 = lRow["ADESC2"].ToString();
                mod.InstitucionId = lRow["InstitucionId"].ToString();
                mod.statusId = Convert.ToInt32(lRow["StatusId"]);
            }
            moders.Add(mod);
            GetCatalogs();
            ViewData["mods"] = moders;
            return View();
        }
        private void SendEmail(string EmailDestino)
        {
            string EmailOrigen = "danchita45@outlook.com";
            string Password = "1106Dani";

            MailMessage mailMessage = new MailMessage(EmailOrigen, EmailDestino, "Asignacion de Evento",
                "<p>Usted Ha sido Asignado para cubrir el evento</p> <br>" +
                "<p>Usted Por Favor De click en el Siguiente Link</p> <br>" +
                "<table><tr>" +
                "<td>" +
                "Nombre:" +
                "<td>" +
                "<tr> <table>");
            mailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com");
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Port = 587;
            smtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Password);
            smtpClient.Send(mailMessage);
            smtpClient.Dispose();
        }






    }
}
