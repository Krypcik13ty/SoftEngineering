using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;

namespace SoftEngineering.Controllers
{
    public class TemporaryController : Controller
    {
        // GET: Temporary
        public ActionResult EmailSender()
        {
            //sendMail();
            return View();
        }

        public ActionResult sendMail()
        {
            string host = "smtp.gmail.com";
            SmtpClient client = new SmtpClient(host, 587);
            client.Credentials = new NetworkCredential("21edqcds@gmail.com", "P@ssw0rd_");
            client.EnableSsl = true;
            MailAddress from = new MailAddress("21edqcds@gmail.com");

            MailAddress to = new MailAddress("dasad44_79@o2.pl");
            MailMessage message = new MailMessage(from, to);
            message.Body = "Kghkjhhjdjd";
            message.Subject = "Nfsafam sfaIe ! ;*";
            client.Send(message);

            client.Dispose();
            return View("EmailSender");
        }
    }
}