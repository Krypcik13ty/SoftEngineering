using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftEngineering.Controllers
{
    public class AccmanagmentController : Controller
    {
        // GET: Accmanagment
        public ActionResult Accmanagment()
        {
            string Username = Session["username"] as string;
            if (Username == null)
            {
                return Redirect("../Log/index");
            }
            return View();
        }
    }
}