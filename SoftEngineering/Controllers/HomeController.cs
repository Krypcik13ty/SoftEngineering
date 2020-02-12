using SoftEngineering.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace SoftEngineering.Controllers
{
    public class HomeController : Controller
    {
        string subjectquery = "SELECT Subject FROM SUBJECTS_DICTIONARY ";
        [HttpGet]

        // GET: Home

        public ActionResult ManualTimetable()
        {
            List<string> subjectList = new List<string>();
            DBConnection dbconnection = new DBConnection();
            dbconnection.ConnectionToList(subjectquery, subjectList);
            ViewData["subjects"] = subjectList;
            return View();
        }

        public ActionResult LogOut()
        {
            return View();
        }
        public ActionResult ConfirmedReg()
        {
            return View();
        }
    }
}