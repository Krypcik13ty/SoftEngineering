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
        string grquery = "SELECT GroupName FROM groups ";
        [HttpGet]

        // GET: Home

        public ActionResult ManualTimetable()
        {
            List<string> subjectList = new List<string>();
            List<string> grouplist = new List<string>();
            DBConnection dbconnection = new DBConnection();
            dbconnection.ConnectionToList(subjectquery, subjectList);
            dbconnection.ConnectionToList(grquery, grouplist);
            ViewData["subjects"] = subjectList;
            ViewData["groups"] = grouplist;
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