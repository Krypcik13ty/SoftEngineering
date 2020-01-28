using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Timers;

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
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        static System.Timers.Timer timer = new System.Timers.Timer();

        // This is the method to run when the timer is raised.
        private static void TimerEventProcessor(Object myObject,
                                                EventArgs myEventArgs)
        {
            myTimer.Stop();

            // Displays a message box asking whether to continue running the timer.
            if (MessageBox.Show("Continue running?", "Count is: ",
              MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Restarts the timer.
                timer.Enabled = true;
            }
            else
            {
                // Stops the timer.
                timer.Enabled = false;
            }
        }

        public static int GetTimer()
        {
            timer = new System.Timers.Timer(5000);
            timer.Elapsed += TimerEventProcessor;

            timer.Enabled = true;

            // Runs the timer, and raises the event.
            System.Windows.Forms.Application.DoEvents();
            return 0;
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