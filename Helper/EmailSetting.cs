using CompanyG02.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace CompanyG02.PL.Helper
{
    public class EmailSetting
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            //Client.Credentials = new NetworkCredential("mh0056592@gmail.com", "iwsfnpvakfvbbjjp");//App Password
            //	Client.Send("mh0056592@gmail.com", email.To, email.Subject,email.Body);
            Client.Credentials = new NetworkCredential("mohamedhassanweb@gmail.com", "dnfq mccz ghwf sksz");//App Password
            Client.Send("mohamedhassanweb@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
