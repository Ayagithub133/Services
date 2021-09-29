using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Services.Server.DataAccessLayer;
using Services.Server.Helper;
using Services.Server.Models;
using Services.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IUserRepository<ApplicationUser> _user;
        private ApplicationDbContext _dbContext;



        public ChatController(IUserRepository<ApplicationUser> user,
            ApplicationDbContext dbContext
            )
        {
            _user = user;
            _dbContext = dbContext;

        }


        [HttpPost("SendMessage")]
        public async Task<ActionResult> sendMessage([FromBody] Messages messages)
        {
            ChatMessage message = new ChatMessage()
            {
                Message = messages.Message,
                Id = Guid.NewGuid().ToString(),
                CreateDate = DateTime.Now,
                FromUserId = CurrentUser.Id,
                ToUserId = messages.ToUserId,
                ToUser = await _user.FindByCondition(user => user.Id == messages.ToUserId),
                FromUser = await _user.FindByIdAsync(CurrentUser.Id)
            };

            await _dbContext.ChatMessage.AddAsync(message);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("History")]
        public async Task<ActionResult<IEnumerable<Messages>>> getMessagesHistory([FromQuery] string reciverId)
        {
            
            var messages = await _dbContext.ChatMessage.Where(x => x.FromUserId == CurrentUser.Id && x.ToUserId == reciverId || x.FromUserId == reciverId && x.ToUserId ==CurrentUser.Id).OrderBy(y=>y.CreateDate).ToListAsync();

            return Ok(messages);
        }

        [HttpGet("getChatWith")]
        public async Task<ActionResult<UserChatDetails>> getChatWith([FromQuery] string Id)
        {
            var user = await _user.FindByIdAsync(Id);
            var chatUser = new UserChatDetails
            {
                UserImage = user.UserImage,
                UserName = user.FirstName + " " + user.LastName,
                isActive = user.isActive
            };

            return Ok(chatUser);
        }


    }
} 
