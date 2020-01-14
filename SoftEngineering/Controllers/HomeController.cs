using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Windows;
using MySql.Data.MySqlClient;
using SoftEngineering.Models;
using System.Timers;
using System.Threading.Tasks;

namespace SoftEngineering.Controllers
{
    public class HomeController : Controller
    {
       
        // GET: Home
        [HttpGet]
        public ActionResult View1()
        {           
            return View();
        }
        
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            /*HOW IT WORKS:
             1. Takes login and password from "index" view
             2. makes connection with DB using connectToDB (created by my boi Dawid Sokół)
             3. From connectToDB returns string array containing username, password and social status
             4. Checks if the array is empty, if it is, returns the fact that you screwed up :)
             5. If the username is correct, checks if password is ok
             6. If password is ok, forwards to ManualTimetable.                         
             */
            string[] array = connectToDB(querymaker(username));
            if (array.Length == 0)
            {
                MessageBox.Show("Incorrect username");
                return View("index");
            }
            else if (array[3] == password)
            {
                MessageBox.Show("Success!");
                return View("ManualTimetable");
            }
            return View("index");
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
        private void OpenConnection(MySqlConnection databaseConnection)
        {
            try
            {
                databaseConnection.Open();
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
            }
        }

        private void CloseConnection(MySqlConnection databaseConnection)
        {
            databaseConnection.Close();
        }
        private string connectionString = "datasource=127.0.0.1; port=3306; username=root; password=; database=testt; CharSet=utf8";
        public string[] connectToDB(string query)
        {
            /*
             * # 2 STEPS to connect to database:
             * #1
            in my.ini file in xampp you must change these lines
            character-set-server=utf8
            collation-server=utf8_general_ci
            --------------
             * #2
             * In database 'Metoda porównywania napisów' = 'utf8_general_ci'	
            */
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            OpenConnection(databaseConnection);

            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;
            try
            {
                reader = commandDatabase.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // As our database, the array will contain : ID 0, FIRST_NAME 1,LAST_NAME 2, ADDRESS 3
                        string[] row = { reader.GetString(0), reader.GetString(1) };
                        return row;
                    }                  
                }
                else
                {
                    Console.WriteLine("No rows found.");
                    return null;
                }
                CloseConnection(databaseConnection);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            return null;

        }
        private string CheckUserType(string[] row)
        {
            if (row[1] == "Admin")
            {
                return "admin";
            }
            else if (row[1] == "Wykładowca")
            {
                return "wykladowca";
            }
            else
            {
                return "User";
            }
        }
        private string querymaker(string login)
        {       
            string query = "SELECT login, haslo, typ FROM accounts WHERE login=";
            string querycomplete = query + "'" + login + "'";
            return querycomplete;
           
        }
    }
}