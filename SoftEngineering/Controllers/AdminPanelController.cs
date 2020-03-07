using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using SoftEngineering.Models;

namespace SoftEngineering.Controllers
{
    public class AdminPanelController : Controller
    {
        // GET: AdminPanel
        DBConnection dbconnection = new DBConnection();
        public ActionResult AdminPanel()
        {
            string Username = Session["username"] as string;
            if (Username == null)
            {
                return Redirect("../Log/index");
            }
            string lecturerquery = "SELECT Login AS lecturer FROM lecturers";
            string subjectQuery = "SELECT Subject FROM subjects_dictionary";
            string classesQuery = "SELECT ClassNR FROM classes";
            string hoursQuery = "SELECT CONVERT(hours_dictionary.Hour, char) as hour FROM hours_dictionary";
            List<string> lecturersList = new List<string>();
            List<string> hoursList = new List<string>();
            List<string> subjectList = new List<string>();
            List<string> classesList = new List<string>();
            dbconnection.ConnectionToList(lecturerquery, lecturersList);
            dbconnection.ConnectionToList(hoursQuery, hoursList);
            dbconnection.ConnectionToList(subjectQuery, subjectList);
            dbconnection.ConnectionToList(classesQuery, classesList);
            ViewData["lecturers"] = lecturersList;
            ViewData["hours"] = hoursList;
            ViewData["subjects_dictionary"] = subjectList;
            ViewData["classes"] = classesList;
            return View();
        }

        [HttpPost]
        public ActionResult SubjectAdd(SubjectAdder model)
        {
            string Username = Session["username"] as string;
            if (Username == null)
            {
                return Redirect("../Log/index");
            }
            string subAdd = "INSERT INTO subjects_dictionary(Subject) VALUES ('" + model.NewSubject + "');";
            DBConnection dbconnection = new DBConnection();
            dbconnection.ExecuteQuery(subAdd);

            string lecturerquery = "SELECT Login AS lecturer FROM lecturers";
            string subjectQuery = "SELECT Subject FROM subjects_dictionary";
            string classesQuery = "SELECT ClassNR FROM classes";
            string hoursQuery = "SELECT CONVERT(hours_dictionary.Hour, char) as hour FROM hours_dictionary";
            List<string> lecturersList = new List<string>();
            List<string> hoursList = new List<string>();
            List<string> subjectList = new List<string>();
            List<string> classesList = new List<string>();
            dbconnection.ConnectionToList(lecturerquery, lecturersList);
            dbconnection.ConnectionToList(hoursQuery, hoursList);
            dbconnection.ConnectionToList(subjectQuery, subjectList);
            dbconnection.ConnectionToList(classesQuery, classesList);
            ViewData["lecturers"] = lecturersList;
            ViewData["hours"] = hoursList;
            ViewData["subjects_dictionary"] = subjectList;
            ViewData["classes"] = classesList;
            return View("AdminPanel");
        }

        [HttpPost]
        public ActionResult SubjectEdit(SubEdit model)
        {
            string Username = Session["username"] as string;
            if (Username == null)
            {
                return Redirect("../Log/index");
            }
            string subEdit = "UPDATE schedule SET Class=('" + model.Change4 + "'),Lecturer=('" + model.Change1 + "'),DateId=('" + model.Change3 + "')" +
                "Where Class=('" + model.Ch4 + "') AND Lecturer=('" + model.Ch1 + "') AND DateId=('" + model.Ch3 + "');";
            DBConnection dbconnection = new DBConnection();
            dbconnection.ExecuteQuery(subEdit);

            string lecturerquery = "SELECT Login AS lecturer FROM lecturers";
            string subjectQuery = "SELECT Subject FROM subjects_dictionary";
            string classesQuery = "SELECT ClassNR FROM classes";
            string hoursQuery = "SELECT CONVERT(hours_dictionary.Hour, char) as hour FROM hours_dictionary";
            List<string> lecturersList = new List<string>();
            List<string> hoursList = new List<string>();
            List<string> subjectList = new List<string>();
            List<string> classesList = new List<string>();
            dbconnection.ConnectionToList(lecturerquery, lecturersList);
            dbconnection.ConnectionToList(hoursQuery, hoursList);
            dbconnection.ConnectionToList(subjectQuery, subjectList);
            dbconnection.ConnectionToList(classesQuery, classesList);
            ViewData["lecturers"] = lecturersList;
            ViewData["hours"] = hoursList;
            ViewData["subjects_dictionary"] = subjectList;
            ViewData["classes"] = classesList;
            return View("AdminPanel");
        }

    }
}