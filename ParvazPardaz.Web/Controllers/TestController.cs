using ParvazPardaz.EmailSendig;
using ParvazPardaz.Payamak.kavenegar;
using ParvazPardaz.ViewModel;
using ParvazPardaz.ViewModel.Payamak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParvazPardaz.Web.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/
        public ActionResult Email()
        {
            string recipient = "ce.safdari@gmail.com";
            //var emailSend = new WelcomeEmail();
            dynamic emailSend = new Postal.Email("Welcome.Html");           
            emailSend.Subject = "به کی سفر خوش آمدید";
            emailSend.Name = "حسین صفدری";            
            emailSend.To = recipient;
            emailSend.Send();
            return View();
        }
        public ActionResult Payamak()
        {
            MessageViewModel obj = new MessageViewModel();
            obj.Text = "سلام";
            obj.Sender = "0013658000175";
            obj.Receptor = "09121869187";
            obj.Text = "پیام آزمایشی وب سایت کی سفر";
            var result = MessageKavenegar.Send(obj);
            return View("Email");
        }
        public ActionResult Email2()
        {
            //dynamic email = new Email("MyTest");
            //email.ViewName = "MyTest";
            //email.To = "ce.safdari@gmail.com,bh.azizi@gmail.com";
            //email.From = "noreply@parvazpardaz.com";
            //email.Subject = "به کی سفر خوش آمدید";
            //TempData["UserName"] = "حسین صفدری";
            //email.Send();
            //return View("Email");
            throw new NotImplementedException();
        }
    }
}