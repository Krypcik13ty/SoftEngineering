using SoftEngineering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftEngineering.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactMail()
        {
            MailSender mailsender = new MailSender();
            mailsender.SendMail();
            return View("Contact");
        }

    }
}