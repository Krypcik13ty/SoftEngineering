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
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Contact()
        {
            string Username = Session["username"] as string;
            if (Username == null)
            {
                return Redirect("../Log/index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult ContactMail(MailSender model)
        {
            string Username = Session["username"] as string;
            if (Username == null)
            {
                return Redirect("../Log/index");
            }
            MailSender mailsender = new MailSender();
            mailsender.mailadress = model.mailadress;
            mailsender.mailtext = model.mailtext;
            mailsender.firstname = model.firstname;
            mailsender.lastname = model.lastname;
            mailsender.mailsubject = model.mailsubject;
            mailsender.SendMail();
            return View("Contact");
        }

    }
}