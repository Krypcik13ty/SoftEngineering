using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using SoftEngineering.Models;

namespace SoftEngineering.Controllers
{
    public class LogController : Controller
    {
        // GET: Log
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Logging user)
        {
            DBConnection dbconnection = new DBConnection();
            string[] array = dbconnection.connectToDB(follog(user.Username));

            if (user.Username == array[0])
            {

                if (array[1] == user.Password)
                {

                    Session["username"] = user.Username;

                    return Redirect("../Home/ManualTimetable");
                }
                MessageBox.Show("Incorrect username");
                return View("index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Passchange(Logging user)
        {
            DBConnection dbconnection = new DBConnection();
            if (user.Password == user.passcheck && user.Newpass == user.Newpasscheck)
            {
                string[] array = dbconnection.connectToDB(passchange(user.Newpass, user.Username));
                MessageBox.Show("Complete!");
                return View("ManualTimetable");
            }
            MessageBox.Show("Something went wrong! Please confirm if credentials you've put in are correct.");
            return View("Accmanagment");
        }

        private string follog(string login)
        {
            string query = "SELECT * FROM ACCOUNTS WHERE Login = '" + login + "'";
            //string querycomplete = query + "'" + login + "'";
            return query;
        }

        private string passchange(string Newpass, string Username)
        {
            string query = "UPDATE ACCOUNTS SET Haslo = '" + Newpass + "'WHERE Login = '" + Username + "'";
            return query;
        }

    }
}