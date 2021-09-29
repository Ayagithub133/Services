
using Services.Server.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.Clients;
using System.Net.Http;
using Twilio.Http;

using Microsoft.Extensions.Configuration;

namespace Services.Server.Services
{

    public class TwilioClient : ITwilioRestClient
    {
        private readonly ITwilioRestClient _innerClient;
        public TwilioClient(IConfiguration config, System.Net.Http.HttpClient httpClient)
        {
            // customize the underlying HttpClient
            httpClient.DefaultRequestHeaders.Add("X-Custom-Header", "CustomTwilioRestClient-Demo");
            _innerClient = new TwilioRestClient(
                TwilioConnect.AccountSid,
                TwilioConnect.AuthToken,
                httpClient: new SystemNetHttpClient(httpClient));
        }
        
           public Response Request(Request request) => _innerClient.Request(request);
        
        public Task<Response> RequestAsync(Request request) => _innerClient.RequestAsync(request);
        public string AccountSid => _innerClient.AccountSid;
        public string Region => _innerClient.Region;
        public Twilio.Http.HttpClient HttpClient => _innerClient.HttpClient;
    }
}
