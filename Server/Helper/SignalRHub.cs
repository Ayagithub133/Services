using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Services.Server.Models;
using Services.Shared.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Twilio.Http;


namespace Services.Server.Helper
{
    public class SignalRHub : Hub
    {
        
        public static List<UserDetails> userDetails = new List<UserDetails>();
      

        public async Task SendMessage(Messages message) 
        {

            var ids = userDetails.Where(x => x.UserId == CurrentUser.Id || x.UserId == message.ToUserId).Select(x=>x.ConnectionId).ToList();
            await Clients.Clients(ids).SendAsync("ReceiveMessage", message);
        }
  
        public async Task AddComment(OfferViewModel offer)
        {
            await Clients.All.SendAsync("ReceiveComment", offer);
        }

        public void connect()
        {
            string connectionId = Context.ConnectionId;
            if(userDetails.Where(x=>x.ConnectionId == connectionId).FirstOrDefault()==null)
            {
                userDetails.Add(new UserDetails {
                    ConnectionId = connectionId,
                    UserId = CurrentUser.Id
                });
            }

            
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var users = userDetails.ToList();
            var user = users.Where(x => x.UserId == CurrentUser.Id).FirstOrDefault();
            if(user !=null)
            userDetails.Remove(user);

            return base.OnDisconnectedAsync(exception);
            
        }
    }
}
 