using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Services.Server.Models;
using Services.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Services.Server.Services;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Services.Server.Mapper;
using NETCore.MailKit.Core;

using Org.BouncyCastle.Asn1.Ocsp;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Services.Server.Controllers;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;

namespace Services.Server.DataAccessLayer
{
    public class UserRepository : IUserRepository<ApplicationUser>
    {

        private IConfiguration _config;
        private ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _user;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMailService _mail;
        private readonly IEmailService _emailService;
        private readonly ISMS _sms;
        


        public UserRepository(ApplicationDbContext dbContext,
             UserManager<ApplicationUser> user, ISMS sms,
             IConfiguration config, IMailService mail, IEmailService emailService,
             IHttpContextAccessor httpContext)
        {
            _dbContext = dbContext;
            _sms = sms;
            _user = user;
            _httpContext = httpContext;
            _config = config;
            _mail = mail; ;
            _emailService = emailService;
        }



        public async Task<UserManagerResponse> Add(ApplicationUser entity)
        {
            _dbContext.Set<ApplicationUser>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return new UserManagerResponse()
            {
                Message = "تمت الاضافة بنجاح",
                IsSuccess = true
            };
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> FindByCondition(Expression<Func<ApplicationUser, bool>> predicate)
        {
            return await _dbContext.Set<ApplicationUser>().Where(predicate).FirstOrDefaultAsync();
        }
        public async Task UpdateByIdAsync(string id)
        {
            var user = await _dbContext.Set<ApplicationUser>().FindAsync(id);
            user.PhoneNumberConfirmed = true;
            await this.Update(user);

        }

        public async Task<IEnumerable<ApplicationUser>> GetAll()
        {
            var users = await _dbContext.Set<ApplicationUser>().ToListAsync();
            return users;
        }

        public async Task<UserManagerResponse> Update(ApplicationUser entity)
        {

            _dbContext.Entry(entity).State = EntityState.Modified;
            
                await _dbContext.SaveChangesAsync();
           
            return new UserManagerResponse
            { 
                
                IsSuccess = true
            };
        }

        
        public async Task<UserManagerResponse> RegisterUserAsyn(RegisterViewModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException("النموذج فارغ");
            }

           else if (model.Password != model.ConfirmPassword)
            {
                return new UserManagerResponse()
                {
                    Message = "كلمة المرور و تأكيد كلمة المرور غير متطابقان",
                    IsSuccess = false,
                };
            }
           
            var appUser = new ApplicationUser
            {
                
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                LastName = model.LastName,
                Email = model.Email,
                PasswordHash = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(model.Password)),
                PhoneNumber = model.Phone,
                CreateAt = DateTime.Now.Date,
                Id = Guid.NewGuid().ToString(),
                
            };
            //  var user = _mapper.Map<ApplicationUser>(model);



            var result2 = await this.Add(appUser);
            if (result2.IsSuccess)
            {

                await _user.AddToRoleAsync(appUser, Roles.User.ToString());

                return new UserManagerResponse {
                    Message = "تم انشاء الحساب بنجاح",
                    IsSuccess = true,
                    Id = appUser.Id
                };
            }
            return new UserManagerResponse
            {
                Message = "فشل انشاء الحساب",
                IsSuccess = false,
                //Errors = result2.Errors.Select(e => e.FirstOrDefault().ToString())
            };
        }


        public async Task<UserManagerResponse> ConfirmEmailAsync(string userId)
        {
            var user = await _user.FindByIdAsync(userId);
            if (user == null)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "المستخدم غير موجود"
                };
            else
            {
                user.EmailConfirmed = true;
                await this.Update(user);
                
                return new UserManagerResponse
                {
                    IsSuccess = true,
                    Message = "تم تفعيل الحساب",
                    Id = userId
                };
            }
        }

             public async Task<UserManagerResponse> ConfirmEmailAsync(string userId, string Token)
             {
                 var user = await _user.FindByIdAsync(userId);
                 if (user == null)
                     return new UserManagerResponse
                     {
                         IsSuccess = false,
                         Message = "المستخدم غير موجود"
                     };

                 var decodedToken = WebEncoders.Base64UrlDecode(Token);
                 string normatToken = Encoding.UTF8.GetString(decodedToken);
                 var result = await _user.ConfirmEmailAsync(user, normatToken);

                 if (result.Succeeded)
                     return new UserManagerResponse
                     {
                         Message = "تم تفعيل الحساب بنجاج",
                         IsSuccess = true
                     };
                 return new UserManagerResponse
                 {
                     IsSuccess = false,
                     Message = "لم يتم تفعيل الحساب",

                 };
             }

            public async Task<UserManagerResponse> LoginUserAsync(LoginViewModel model)
        {
            var user = await this.FindByCondition(x => x.PhoneNumber == model.PhoneNumber);
            var PasswordHash = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(model.Password));
            if (user == null || user.PasswordHash != PasswordHash)
            {
                return new UserManagerResponse
                {
                    Message = "رقم الهاتف أو كلمة المرور غير صحيحة",
                    IsSuccess = false,
                };

            }
            else
            {
                if(!user.EmailConfirmed)
                {
                    return new UserManagerResponse
                    {
                        Message = "البريد الالكترونى غير مفعل",
                        IsSuccess = false,
                    };
                }
                return new UserManagerResponse
                {
                    Id = user.Id,
                    IsSuccess = true

                };
            }

        }

        public ApplicationUser FindEntityByCondition(Expression<Func<ApplicationUser, bool>> predicate)
        {
            return  _dbContext.Set<ApplicationUser>().Where(predicate).FirstOrDefault();
        }

        public async Task<ApplicationUser> FindByIdAsync(string Id)
        {
            var user = await _dbContext.Set<ApplicationUser>().FindAsync(Id);
            return user;
        }

        public async Task ActivationUpdateAsync(string id , bool value)
        {
               var entity = await _dbContext.Set<ApplicationUser>().Where(x=>x.Id == id).FirstOrDefaultAsync();
            
                entity.isActive = value;
               _dbContext.Entry(entity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
        }
    }
}
