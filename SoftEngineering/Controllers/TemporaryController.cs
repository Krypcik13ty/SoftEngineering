using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Windows;
using System.Timers;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace SoftEngineering.Controllers
{
    public class TemporaryController : Controller
    {
        private string connectionString = "datasource=127.0.0.1; port=3306; username=root; password=; database=testt; CharSet=utf8";
        private string query = "SELECT login, typ FROM accounts";
        // GET: Temporary
        public ActionResult TemporaryView()
        {
            GetTimer();
            return View();
        }

        public ActionResult sendMail()
        {
            string host = "smtp.gmail.com";
            SmtpClient client = new SmtpClient(host, 587);
            client.Credentials = new NetworkCredential("21edqcds@gmail.com", "P@ssw0rd_");
            client.EnableSsl = true;
            MailAddress from = new MailAddress("21edqcds@gmail.com");
            try
            {
                MailAddress to = new MailAddress("radziomenek@gmail.com");
                MailMessage message = new MailMessage(from, to);

                message.Body = "Kghkjhhjdjd";
                message.Subject = "Nfsafam sfaIe ! ;*";
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
            return View("TemporaryView");
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

        public ActionResult connectToDB()
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
                        MessageBox.Show(CheckUserType(row));
                    }
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

            return View("TemporaryView");
        }
        public static void GetTimer()
        {
            Timer timer = new Timer(10000);
            timer.Elapsed += async (sender, e) => await HandleTimer();
            timer.Start();
        }
        private static Task HandleTimer()
        {
            MessageBox.Show("Wyslano maila");
            throw new NotImplementedException();
        }

        private string CheckUserType(string[] row)
        {
            if(row[1] == "Admin")
            {
                return "admin";
            }
            else if(row[1] == "Wykładowca")
            {
                return "wykladowca";
            }
            else
            {
                return "User";
            }
        }
    }
}