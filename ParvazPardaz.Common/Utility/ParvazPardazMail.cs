using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ParvazPardazMail
    {
        public void SendGeneratedMessage(string mContetnt, string emailRecivers, string mSubject)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"]);
            mail.To.Add(emailRecivers);
            mail.Subject = mSubject;
            mail.Body = mContetnt;
            mail.IsBodyHtml = true;
            //add header

            //ad priority
            mail.Priority = System.Net.Mail.MailPriority.High;
            //send mail
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials =
            new System.Net.NetworkCredential(ConfigurationManager.AppSettings["MailFrom"], ConfigurationManager.AppSettings["MailFromPassword"]);
            smtp.Port = Int32.Parse(ConfigurationManager.AppSettings["MailFromPort"]);//587:Local 25:Host;
            smtp.Host = ConfigurationManager.AppSettings["MailFromHost"];
            smtp.EnableSsl = false;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            try
            {
                smtp.Send(mail);
            }
            catch (Exception)
            {

            }

        }

    }
}
