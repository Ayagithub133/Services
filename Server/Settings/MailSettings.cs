using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Server.Settings
{
    public class MailSettings
    {
        public string Email { set; get; } = "oservices71@gmail.com";
        public string DisplayName { set; get; } = "خدمات اونلاين";
        public string Password { set; get; } = "OnlineServ099987776555";
        public string Host { set; get; } = "smtp.gmail.com";
        public int Port { set; get; } = 465;
    }


}
