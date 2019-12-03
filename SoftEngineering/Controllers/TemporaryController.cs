using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Windows;

namespace SoftEngineering.Controllers
{
    public class TemporaryController : Controller
    {
        // GET: Temporary
        public ActionResult TemporaryView()
        {
            //sendMail();
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

        public ActionResult connectToDB()
        {
            string connetionString = null;
            SqlConnection connection;
            SqlCommand command;
            string sqlcommand = "SELECT * from Accounts";

            connetionString = "Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password";

            connection = new SqlConnection(connetionString);
            try
            {
                connection.Open();
                command = new SqlCommand(sqlcommand, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                MessageBox.Show("Połączono");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd połączenia");
            }
            return View("TemporaryView");
        }
    }
}