using Microsoft.Extensions.Configuration;
using NETCore.MailKit.Core;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace Services.Server.Services
{
    public class SendGridMailService : IMailService
    {

        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;

        public SendGridMailService(IConfiguration config, IEmailService emailService)
        {
            _config = config;
            _emailService = emailService;
        }

        public async Task sendMailAsync(string toEmail, string subject, string content)
        {
            var apiKey = _config["SendGridAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("ayaa.mohamed133@gmail.com", "01113510466");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
