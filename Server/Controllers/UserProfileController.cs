using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Services.Server.DataAccessLayer;
using Services.Server.Features;
using Services.Server.Helper;
using Services.Server.Models;
using Services.Shared.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IWebHostEnvironment _host;
        private readonly IUserRepository<ApplicationUser> _userRepo;
        private readonly IRepository<Hall> _hallRepo;
        private readonly IRepository<Offers> _offerRepo;
        public UserProfileController(IUserRepository<ApplicationUser> userRepo,
            IRepository<Hall> hallRepo, IRepository<Offers> offerRepo,
            IWebHostEnvironment host)
        {
            _userRepo = userRepo;
            _hallRepo = hallRepo;
            _offerRepo = offerRepo;
            _host = host;
        }

        [HttpGet("userprofile")]
        public async Task<ActionResult<ProfileUser>> GetProfile ([FromQuery] string id)
        {
            ApplicationUser user = new ApplicationUser();
            var currentUserId = CurrentUser.Id;
            if (id == null)
            {
                 user = await _userRepo.FindByCondition(x => x.Id == currentUserId);

            }
            else
            {
                user = await _userRepo.FindByCondition(x => x.Id == id);
            }
            ProfileUser profile = new ProfileUser
            {
                CreateAt = user.CreateAt,
                UserName = user.FirstName + " " + user.SecondName,
                UserImage = user.UserImage,
                ServiceCount = await _hallRepo.CountOfRows(currentUserId),
                OffersCount = await _offerRepo.CountOfRows(currentUserId),
                isActive = user.isActive
                 
            };
            
            return Ok(profile);
        }

        [HttpGet("useroffers")]
        public async Task<ActionResult<IEnumerable<Offers>>> GetUserOffers([FromQuery] PaginationParameters pagination)
        {
            var currentUserId = CurrentUser.Id;
            var offers =  _offerRepo.GetTableAsync().Where(x => x.UserId == currentUserId);
            await HttpContext.INsertPaginationParametersInResponse(offers, pagination.QuantityPerPage);
            var entity =  offers.Paginate(pagination).ToList();

            List<OfferViewModel> offerViewModels = new List<OfferViewModel>();
            foreach(var item in entity)
            {
                offerViewModels.Add(
                    new OfferViewModel
                    {
                        
                        HallID = item.HallID,
                        TextOffer = item.TextOffer,
                        AddTime = item.AddTime,
                        Budget=item.Budget,
                        ServiceID = item.ServiceID
                    }
                    );
            }
            if (offers.Count() != 0)
            {
                return StatusCode(200, offerViewModels);
            }
            else return StatusCode(205, offerViewModels);
        }

        [HttpGet("userservicess")]
        public async Task<ActionResult<IEnumerable<HallViewModelResponse>>> GetUserServices([FromQuery] PaginationParameters pagination)
        {
            var currentUserId = CurrentUser.Id;
            var services = _hallRepo.GetTableAsync().Where(x => x.UserId == currentUserId);
            await HttpContext.INsertPaginationParametersInResponse(services, pagination.QuantityPerPage);
            var entity = services.Paginate(pagination).ToList();

            List<HallViewModelResponse> hallViewModels = new List<HallViewModelResponse>();
            foreach (var item in entity)
            {
                hallViewModels.Add(
                    new HallViewModelResponse
                    {

                      HallDescription = item.HallDescription,
                      Id = item.Id,
                      TimeAdd = item.TimeAdd
                    }
                    );
            }
            if (services.Count() != 0)
            {
                return StatusCode(200, hallViewModels);
            }
            else return StatusCode(205, hallViewModels);
        }


        [HttpGet("currentuser")]
        public async Task<ActionResult<UpdateProfileRequest>> GetUser()
        {
            var user = await _userRepo.FindByIdAsync(CurrentUser.Id);
            if(user != null)
            {
                UpdateProfileRequest profileRequest = new UpdateProfileRequest
                { 
                    FirstName = user.FirstName,
                    SecondName = user.SecondName,
                    Email = user.Email,
                    PhoneNumber= user.PhoneNumber,
                    Image = user.UserImage
                };
                return StatusCode(200, profileRequest);
            }
            return StatusCode(205);
        }
   

        [HttpPost("editprofile")]
        public async Task<ActionResult<UserManagerResponse>> EditProfile([FromBody] UpdateProfileRequest profile)
        {
            if(ModelState.IsValid)
            {
                   string ImageName = Path.GetFileName(profile.Image);
                   string extension = Path.GetExtension(ImageName);
                    string[] allowedExtensions = { ".jpg", ".png", ".bmp" };
                    if(!allowedExtensions.Contains(extension))
                          return Ok(new UserManagerResponse {
                              Message = "اختر صورة تحتوى على احدى الامتدادات (.png , .jpg, .bmp)" 
                          });
                     string newFileName = $"{Guid.NewGuid()}{extension}";
                     string filePath = Path.Combine($"Uploads",newFileName);

                var user = await _userRepo.FindByCondition(x => x.Id == CurrentUser.Id);

                user.FirstName = profile.FirstName;
                user.SecondName = profile.SecondName;
                user.Email = profile.Email;
                user.PhoneNumber = profile.PhoneNumber;
                user.PasswordHash = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(profile.Password));
                user.UserImage = filePath;
                user.Id = CurrentUser.Id;

                

               var result = await _userRepo.Update(user);
                if(result.IsSuccess)
                {
                    return new UserManagerResponse
                    {
                        Message = "تم تعديل الحساب بنجاح",
                        IsSuccess=true
                    };
                }
                return new UserManagerResponse
                {
                    Message = "فشل تعديل الحساب"
                   
                };

            }
            return Ok(
                new UserManagerResponse
                {
                    Message = "يوجد خلل فى البيانات المرسلة",
                    IsSuccess = false
                });
            
        }
    }
}
