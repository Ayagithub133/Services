using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Server.Services
{
    public interface IMailService
    {
        Task sendMailAsync(string toEmail, string subject, string content);
    }
}
