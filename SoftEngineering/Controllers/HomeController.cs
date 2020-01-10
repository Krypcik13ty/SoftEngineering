using SoftEngineering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace SoftEngineering.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManualTimetable()
        {
            return View();
        }
        public ActionResult Accmanagment()
        {
            return View();
        }
        public ActionResult AdminPanel()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ContactMail(MailSender model)
        {
            string host = "smtp.gmail.com";
            SmtpClient client = new SmtpClient(host, 587);
            client.Credentials = new NetworkCredential("21edqcds@gmail.com", "P@ssw0rd_");
            client.EnableSsl = true;
            MailAddress from = new MailAddress("21edqcds@gmail.com");
            try
            {
                MailAddress to = new MailAddress(model.mailadress);
                MailMessage message = new MailMessage(from, to);

                message.Body = model.mailtext;
                message.Subject = model.mailsubject + " " + model.firstname + " " + model.lastname;
                client.Send(message);
                client.Dispose();
                MessageBox.Show("Wyslano maila");
            }
            catch (FormatException e)
            {
                MessageBox.Show("Zły format maila");
            }
            catch (Exception e)    //other exceptions
            {
                MessageBox.Show("Błąd podczas wysyłania miaila");
            }
            return View("Contact");
        }
    }
}