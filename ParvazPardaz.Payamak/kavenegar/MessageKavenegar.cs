using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kavenegar;
using Kavenegar.Models;
using ParvazPardaz.ViewModel.Payamak;

namespace ParvazPardaz.Payamak.kavenegar
{
    public static class MessageKavenegar
    {


        public static int Send(MessageViewModel message)
        {
            string ApiKey = System.Configuration.ConfigurationManager.AppSettings["ApiKey"];
            var api = new Kavenegar.KavenegarApi(ApiKey);
            var Result = api.Send(message.Sender, message.Receptor, message.Text);
            return Result.Status;
        }
    }
}
