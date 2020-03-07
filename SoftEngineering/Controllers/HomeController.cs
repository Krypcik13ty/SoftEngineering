using SoftEngineering.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web.SessionState;

namespace SoftEngineering.Controllers
{
    public class HomeController : Controller
    {


        string subjectquery = "SELECT Subject FROM SUBJECTS_DICTIONARY ";
        string grquery = "SELECT GroupName FROM groups ";
        string mondayquery = "select subjects_dictionary.Subject, CONVERT(schedule.Start_hour, varchar(255)) as hour from schedule inner join subjects_dictionary on schedule.Subject=subjects_dictionary.ID where schedule.DateId ='" + "2020-01-08'";
        string tuesdayquery = "select subjects_dictionary.Subject, CONVERT(schedule.Start_hour, varchar(255)) as hour from schedule inner join subjects_dictionary on schedule.Subject=subjects_dictionary.ID where schedule.DateId ='" + "2020-01-09'";
        string wednesdayquery = "select subjects_dictionary.Subject, CONVERT(schedule.Start_hour, varchar(255)) as hour from schedule inner join subjects_dictionary on schedule.Subject=subjects_dictionary.ID where schedule.DateId ='" + "2020-01-10'";
        string thursdayquery = "select subjects_dictionary.Subject, CONVERT(schedule.Start_hour, varchar(255)) as hour from schedule inner join subjects_dictionary on schedule.Subject=subjects_dictionary.ID where schedule.DateId ='" + "2020-01-11'";
        string fridayquery = "select subjects_dictionary.Subject, CONVERT(schedule.Start_hour, varchar(255)) as hour from schedule inner join subjects_dictionary on schedule.Subject=subjects_dictionary.ID where schedule.DateId ='" + "2020-01-12'";
        [HttpGet]

        // GET: Home
        //"08:00:00.000000 Plastyka dla Informatyków"
        //"07.01.2020 00:00:0008:15:00.000000 Plastyka dla Informatyków"


        public ActionResult ManualTimetable()
        {
            string Username = Session["username"] as string;
            if (Username == null)
            {
                return Redirect("../Log/index");
            }

       

            List<string> subjectList = new List<string>();
                List<string> subjectsList = new List<string>();
                List<string> grouplist = new List<string>();
                List<string> dayList = new List<string>();

                List<string> hourList = new List<string>();
                List<string> mondaysList = new List<string>();
                List<string> tuesdaysList = new List<string>();
                List<string> wednesdaysList = new List<string>();
                List<string> thursdaysList = new List<string>();
                List<string> fridaysList = new List<string>();
                DBConnection dbconnection = new DBConnection();
                dbconnection.ConnectionToList(subjectquery, subjectList);
                dbconnection.ConnectionToList(grquery, grouplist);
                dbconnection.ConnectionTo3List(mondayquery, subjectsList, hourList);
                for (int i = 0; i < hourList.Count; i++)
                {

                    mondaysList.Add(hourList[i].ToString() + " " + subjectsList[i].ToString());
                }
                subjectsList.Clear();
                hourList.Clear();
                dbconnection.ConnectionTo3List(tuesdayquery, subjectsList, hourList);
                for (int i = 0; i < hourList.Count; i++)
                {

                    tuesdaysList.Add(hourList[i].ToString() + " " + subjectsList[i].ToString());
                }

                subjectsList.Clear();
                hourList.Clear();
                dbconnection.ConnectionTo3List(wednesdayquery, subjectsList, hourList);
                for (int i = 0; i < hourList.Count; i++)
                {
                    wednesdaysList.Add(hourList[i].ToString() + " " + subjectsList[i].ToString());
                }

                subjectsList.Clear();
                hourList.Clear();
                dbconnection.ConnectionTo3List(thursdayquery, subjectsList, hourList);
                for (int i = 0; i < hourList.Count; i++)
                {
                    thursdaysList.Add(hourList[i].ToString() + " " + subjectsList[i].ToString());
                }

                subjectsList.Clear();
                hourList.Clear();
                dbconnection.ConnectionTo3List(fridayquery, subjectsList, hourList);
                for (int i = 0; i < hourList.Count; i++)
                {
                    fridaysList.Add(hourList[i].ToString() + " " + subjectsList[i].ToString());
                }
            
                //kazda lista przechowwuje zajecia z danego dnia
                ViewData["mondays"] = mondaysList;
                ViewData["tuesdays"] = tuesdaysList;
                ViewData["wednesdays"] = wednesdaysList;
                ViewData["thursdays"] = thursdaysList;
                ViewData["fridays"] = fridaysList;

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