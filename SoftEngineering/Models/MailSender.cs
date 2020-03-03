using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Windows;

namespace SoftEngineering.Models
{
    public class MailSender
    {
        [Required]
        public string mailadress { get; set; }
        public string mailsubject { get; set; }
        public string mailtext { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }

        public void SendMail()
        {
            string host = "smtp.gmail.com";
            SmtpClient client = new SmtpClient(host, 587);
            client.Credentials = new NetworkCredential("21edqcds@gmail.com", "P@ssw0rd_");
            client.EnableSsl = true;
            MailAddress from = new MailAddress("21edqcds@gmail.com");
            try
            {
                MailAddress to = new MailAddress(mailadress);
                MailMessage message = new MailMessage(from, to);
                message.Body = mailtext;
                message.Subject = mailsubject + " " + firstname + " " + lastname;
                client.Send(message);
                client.Dispose();
                MessageBox.Show("Wyslano maila");
            }
            catch (FormatException e)
            {
                MessageBox.Show("Zły format maila");
            }
            catch (Exception e)  
            {
                MessageBox.Show("Błąd podczas wysyłania miaila");
            }
        }

    }
}