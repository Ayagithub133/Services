using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Services.Server.Services
{
    public class SMS:ISMS
    {
        private readonly ITwilioRestClient _client;
        public SMS(ITwilioRestClient client)
        {
            _client = client;
        }

        public async Task SendSms(string phone)
        {
        
            SMSCode vertify = new SMSCode();
            //try
            //{
            //    var message = await MessageResource.CreateAsync(to: new PhoneNumber(phone),
            //        from: new PhoneNumber(SmsMessage.From),
            //        body: SMSCode.random + " " + SmsMessage.Message,
            //        client: _client

            //        );
            //}
            //catch(Exception EX)
            //{
            //    var X = EX.Message;
            //}
        }
    }
}
