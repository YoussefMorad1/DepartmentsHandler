using DAL_DataAccessLayer.Models;
using System.Data;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PL_PresentationLayerMVC.Helpers
{
	public class EmailHelper
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.Credentials = new System.Net.NetworkCredential("youssefmohammed747@gmail.com", "krpvrwybmoylccbb");
			client.Send("youssefmohammed747@gmail.com", email.To, email.Subject, email.Body);
		}
	}
}
