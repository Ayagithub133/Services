using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using Services.Server.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Threading.Tasks;

namespace Services.Server.DataAccessLayer
{
    public class MailingService : IMailingService
    {

        private readonly MailSettings _mailSettings;
        public MailingService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

             public async Task SendEmailAsync(string mailTo, string subject, string body, IList<IFormFile> attachments = null)
              {
                  var email = new MimeMessage
                  {
                      Sender = MailboxAddress.Parse(_mailSettings.Email),
                      Subject = subject
                  };

                   email.To.Add(MailboxAddress.Parse(mailTo));

                  var builder = new BodyBuilder();
                  if(attachments != null)
                  {
                      byte[] fileBytes;
                      foreach(var file in attachments)
                      {
                          if(file.Length > 0)
                          {
                              using var ms = new MemoryStream();fileBytes = ms.ToArray();

                              builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                              file.CopyTo(ms);

                          }
                      }
                  }

                  builder.HtmlBody = body;
                  email.Body = builder.ToMessageBody();
                  email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email));

            using var smtp = new SmtpClient();
            smtp.CheckCertificateRevocation = false;
            //  smtp.SslProtocols = System.Security.Authentication.SslProtocols.None;

            try
                  {
                       await  smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.SslOnConnect);
                  }
                  catch(Exception ex)
                  {
                      var x1 = ex.Message;
                      var x2 = ex.StackTrace;
                      var x3 = ex.Source;
                      var x4 = ex.Data;
                      var x5 = ex.ToString();
                      var x6 = ex.HelpLink;
                      var x7 = ex.GetBaseException();
                      var x8 = ex.GetType();
                  }
                      smtp.Authenticate(_mailSettings.Email, _mailSettings.Password);
                  await smtp.SendAsync(email);
                  smtp.Disconnect(true);
              } 
    }
}
