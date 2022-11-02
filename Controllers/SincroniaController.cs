using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace ProyectoModeradores.Controllers
{
    public class SincroniaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Sincronizar(string Email)
        {



            SendEmail(Email);
            return RedirectToAction("Index");

        }
        private void SendEmail(string EmailDestino)
        {
            string EmailOrigen = "danchita45@outlook.com";
            string Password = "1106Dani";

            MailMessage mailMessage = new MailMessage(EmailOrigen, EmailDestino, "Asignacion de Evento",
                "<p>Usted Ha sido Asignado para cubrir el evento</p> <br>" +
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
