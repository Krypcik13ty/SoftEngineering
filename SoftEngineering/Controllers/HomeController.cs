using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            return View();
        }
    }
}