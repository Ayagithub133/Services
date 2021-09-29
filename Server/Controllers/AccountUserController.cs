using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Server.DataAccessLayer;
using Services.Server.Helper;
using Services.Server.Models;
using Services.Server.Services;

using Services.Shared.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Services.Server.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AccountUserController : ControllerBase
    {

        private readonly IHttpContextAccessor _http;
        private readonly IUserRepository<ApplicationUser> _repo;
        private readonly IMailingService _mailservice;
        private readonly ISMS _sms;
       
       
        public AccountUserController(IUserRepository<ApplicationUser> repoAcount,
          UserManager<ApplicationUser> user,IHttpContextAccessor http,
          IMailingService mailservice,ISMS sms)
        {
            _repo = repoAcount;
            _mailservice = mailservice;
            _http = http;
            _sms = sms;
          
           
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserManagerResponse>> Register([FromBody]RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                ApplicationUser user = await _repo.FindByCondition(x => x.Email == model.Email || x.PhoneNumber == model.Phone);
                if (user != null)
                {
                    return BadRequest(new UserManagerResponse {Message= "هذا البريد الالكترونى او رقم الهاتف موجود من قبل" });
                }

                CurrentUser.UserData = JsonConvert.SerializeObject(model);
                await _sms.SendSms(model.Phone);
                return Ok(new UserManagerResponse
                {
                    IsSuccess = true
                });

            }
            return BadRequest(new UserManagerResponse { Message = "بعض المدخلات غير صحيحة" });
        }


 

        [HttpGet("confirmemail")]
        public async Task<ActionResult> ConfirmEmail(string userId)
        {
            if(string.IsNullOrWhiteSpace(userId))
            {
                return Ok();
            }
            var result = await _repo.ConfirmEmailAsync(userId);
            if(result.IsSuccess)
            {
                var user = await _repo.FindByIdAsync(result.Id);

                string contents = null;
                var filePath = $"{Directory.GetCurrentDirectory()}\\Templates\\EmailConfirmed.html";
                using (StreamReader streamReader = new StreamReader(filePath, Encoding.UTF8))
                {
                    contents = await streamReader.ReadToEndAsync();
                    streamReader.Close();
                    contents = contents.Replace("[username]", user.FirstName + " " + user.SecondName).Replace("[email]", user.Email);

                }
                return Content(contents, "text/html");
 
            }
            return Ok();
        }

        [HttpPost("confirmSMSCode")]
        public async Task<ActionResult<UserManagerResponse>> SendSMSCode(Vertification code)
        {
            if (code == null)
                return Ok(new UserManagerResponse
                {
                    Message = "ادخل الكود للتحقق",
                    IsSuccess = false
                }) ;

            else if (code.VertificationCode != SMSCode.random)
            {
                return Ok(new UserManagerResponse
                {
                    Message = "ادخل الكود بشكل صحيح",
                    IsSuccess = false
                });
            }
            else
            {
               var user = JsonConvert.DeserializeObject<RegisterViewModel>(CurrentUser.UserData);
                var result =  await _repo.RegisterUserAsyn(user);
                await SendWelcomeEmail(result.Id);
                return Ok();
            }
            
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginViewModel model)
        {
          
            if(ModelState.IsValid)
            {
             
                var result = await _repo.LoginUserAsync(model);

                if (result.IsSuccess)
                {
                    
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddDays(1);
                    options.Secure = true;


                    _http.HttpContext.Response.Cookies.Append("authCookie", result.Id, options);
                    await _repo.ActivationUpdateAsync(result.Id,true);
                    
                }
                    return Ok(result);
                
    
            }
            return BadRequest(new UserManagerResponse { Message = "بعض المدخلات غير صحيحة" });
        }
        [HttpGet("currentuser")]
        public  ActionResult<UserManagerResponse> GetCurrentUser()
        {

            var Id = Request.Cookies["authCookie"];
            CurrentUser.Id = Id;

           
            if (Id==null)
            {
                return Ok(new UserManagerResponse {IsSuccess = false });
            }
            return  Ok(new UserManagerResponse { IsSuccess = true });
        }


        
        [HttpGet("LogOutUser")]
        public async Task<ActionResult> LogOutUser()
        {
            if (CurrentUser.Id != null)
            {
                await _repo.ActivationUpdateAsync(CurrentUser.Id, false);

                _http.HttpContext.Response.Cookies.Delete("authCookie");
            }
            await HttpContext.SignOutAsync();
            return Ok();
        }




        private async Task SendWelcomeEmail(string userId)
        {
            var user = await _repo.FindByIdAsync(userId);
            var filePath = $"{Directory.GetCurrentDirectory()}\\Templates\\EmailTemplate.html";
            var str = new StreamReader(filePath);

            var mailText = str.ReadToEnd();
            str.Close();

            mailText = mailText.Replace("[username]", user.FirstName +" " +user.SecondName).Replace("[email]", user.Email).Replace("[UserId]",userId);

            await _mailservice.SendEmailAsync(user.Email, "مرحبا بك", mailText);
            
        }
    }
}
