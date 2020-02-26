using SoftEngineering.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;

namespace SoftEngineering.Controllers
{
    public class HomeController : Controller
    {
        string subjectquery = "SELECT Subject FROM SUBJECTS_DICTIONARY ";
        string grquery = "SELECT GroupName FROM groups ";
        string dayquery = "select subjects_dictionary.Subject, schedule.DateId, CONVERT(schedule.Start_hour, varchar(255)) as hour from schedule inner join subjects_dictionary on schedule.Subject=subjects_dictionary.ID";
        [HttpGet]

        // GET: Home
        //"07.01.2020 00:00:00 08:15:00.000000 Plastyka dla Informatyków"
        public ActionResult ManualTimetable()
        {
            List<string> subjectList = new List<string>();
            List<string> subjectsList = new List<string>();
            List<string> grouplist = new List<string>();
            List<string> dayList = new List<string>();
            
            List<string> hourList = new List<string>();
            List<string> daysubjectList = new List<string>();
            DBConnection dbconnection = new DBConnection();
            dbconnection.ConnectionToList(subjectquery, subjectList);
            dbconnection.ConnectionToList(grquery, grouplist);
            dbconnection.ConnectionTo3List(dayquery, subjectsList, dayList, hourList);
            for (int i = 0; i < dayList.Count; i++)
            {
                daysubjectList.Add(dayList[i].ToString() + " " + hourList[i].ToString() + " " + subjectsList[i].ToString());
            }
            ViewData["subjectday"] = daysubjectList;
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