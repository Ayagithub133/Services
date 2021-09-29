using Services.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Server.Services
{
     public interface ISMS
    {
        public  Task SendSms(string phone);
    }
}
