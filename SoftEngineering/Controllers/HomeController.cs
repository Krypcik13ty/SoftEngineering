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
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Logging user)
        {
            /*HOW IT WORKS:
             1. Takes login and password from "index" view
             2. makes connection with DB using connectToDB (created by my boi Dawid Sokół)
             3. From connectToDB returns string array containing username, password and social status
             4. Checks if the array is empty, if it is, returns the fact that you screwed up :)
             5. If the username is correct, checks if password is ok
             6. If password is ok, forwards to ManualTimetable.                         
             */

            string[] array = connectToDB(follog(user.Username));
            if (array[0] == null)
            {
                MessageBox.Show("couldn't get result from DB");
                return View("index");
            }
            if (user.Username == array[0])
            {
               
                if (array[1] == user.Password)
                {
                    user.Type = array[2];
                    return View("ManualTimetable");
                }
                MessageBox.Show("Incorrect username");
                return View("index");
            }
            return View();
        }

        public ActionResult ManualTimetable()
        {
            List<string> subjectList = new List<string>();
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            OpenConnection(databaseConnection);

            MySqlCommand commandDatabase = new MySqlCommand(subjectquery, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;
            try
            {
                reader = commandDatabase.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        subjectList.Add(reader.GetString(0));
                    }
                    ViewData["subjects"] = subjectList;
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                CloseConnection(databaseConnection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return View();

        }
        public ActionResult Accmanagment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Passchange(Logging user)
        {

            if (user.Password == user.passcheck && user.Newpass == user.Newpasscheck)
            {
                string[] array = connectToDB(passchange(user.Newpass, user.Username));
                MessageBox.Show("Complete!");
                return View("ManualTimetable");
            }
            MessageBox.Show("Something went wrong! Please confirm if credentials you've put in are correct.");
            return View("Accmanagment");
        }
        public ActionResult AdminPanel()
        {
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
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ContactMail(MailSender model)
        {
            string host = "smtp.gmail.com";
            SmtpClient client = new SmtpClient(host, 587);
            client.Credentials = new NetworkCredential("21edqcds@gmail.com", "P@ssw0rd_");
            client.EnableSsl = true;
            MailAddress from = new MailAddress("21edqcds@gmail.com");
            try
            {
                MailAddress to = new MailAddress(model.mailadress);
                MailMessage message = new MailMessage(from, to);

                message.Body = model.mailtext;
                message.Subject = model.mailsubject + " " + model.firstname + " " + model.lastname;
                client.Send(message);
                client.Dispose();
                MessageBox.Show("Wyslano maila");
            }
            catch (FormatException e)
            {
                MessageBox.Show("Zły format maila");
            }
            catch (Exception e)    //other exceptions
            {
                MessageBox.Show("Błąd podczas wysyłania miaila");
            }
            return View("Contact");
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
        private string connectionString = "Server=sql.freeasphost.net/MSSQL2016; Database=klossik_schedule; uid=klossik; pwd=haslo123";
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
            else if (row[1] == "Wykladowca")
            {
                return "wykladowca";
            }
            else
            {
                return "User";
            }
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