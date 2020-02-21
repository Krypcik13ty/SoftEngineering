using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoftEngineering.Models;

namespace SoftEngineering.Controllers
{
    public class AdminPanelController : Controller
    {
        // GET: AdminPanel
        DBConnection dbconnection = new DBConnection();
        public ActionResult AdminPanel()
        {
            string lecturerquery = "SELECT CONCAT(Name, '" + " ', Surname) AS lecturer FROM lecturers";
            List<string> lecturersList = new List<string>();
            dbconnection.ConnectionToList(lecturerquery, lecturersList);
            ViewData["lecturers"] = lecturersList;
            return View();
        }

        [HttpPost]
        public ActionResult SubjectAdd(SubjectAdder model)
        {
            string subAdd = "INSERT INTO subjects_dictionary(Subject) VALUES ('" + model.NewSubject + "');";
            DBConnection dbconnection = new DBConnection();
            dbconnection.ExecuteQuery(subAdd);

            string lecturerquery = "SELECT CONCAT(Name, '" + " ', Surname) AS lecturer FROM lecturers";
            List<string> lecturersList = new List<string>();
            dbconnection.ConnectionToList(lecturerquery, lecturersList);
            ViewData["lecturers"] = lecturersList;
            return View("AdminPanel");
        }


     

    }
}