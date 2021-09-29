using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Server.DataAccessLayer;
using Services.Server.Models;
using Services.Shared.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using Services.Server.Helper;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Services.Server.Controllers
{
  

    [Route("api/[controller]")]
    [ApiController]
    public class HallController : ControllerBase
    {
        private readonly IRepository<Hall> _hallRepo;
        private readonly IWebHostEnvironment _hosting;
        private readonly IUserRepository<ApplicationUser> _currentUser;
        private readonly IRepository<Offers> _offerRepo;

        public HallController(IRepository<Hall> hallRepo,
            IWebHostEnvironment host, IUserRepository<ApplicationUser> currentUser,
            IRepository<Offers> offerRepo)
        {
            _currentUser = currentUser;
            _hallRepo = hallRepo;
            _hosting = host;
            _offerRepo = offerRepo;
           
        }
      
        [HttpPost("addHall")]
        public async Task<ActionResult> AddHall([FromBody] HallViewModel hallModel)
        {
            if (ModelState.IsValid)
            {


                Hall hall = new Hall
                {
                    BookDate = hallModel.BookDate,
                    Budget = hallModel.Budget,
                    BookType = hallModel.BookType,
                    GuestGender = hallModel.GuestGender,
                    HallDescription = hallModel.HallDescription,
                    HallSize = hallModel.HallSize,
                    Menue = hallModel.Menue,
                    GuestNumber = hallModel.GuestNumber,
                    Location = hallModel.Location,
                    SuperVisorFNumber = hallModel.SuperVisorFNumber,
                    SuperVisorMNumber = hallModel.SuperVisorMNumber,
                    ServiceTitle = hallModel.ServiceTitle,
                    UserId = CurrentUser.Id,
                    UserCity = hallModel.UserCity,
                    UserContact = hallModel.ContactNumber,
                    Id = Guid.NewGuid().ToString(),
                    TimeAdd = DateTime.Now


                };
                var result = await _hallRepo.Add(hall);
                if (result.IsSuccess)
                {
                    return Ok(new UserManagerResponse
                    {
                        Message = "تمت الاضافة بنجاح",
                        IsSuccess = true

                    }); ;
                }

                return Ok(new UserManagerResponse
                {
                    Message = "لم يتم الاضافة"
                });

            }
            return Ok(new UserManagerResponse { Message = "يوجد اخطاء فى النموذج المرسل " });

        }

        [HttpGet("getHalls")]
        public async  Task<ActionResult<IEnumerable<HallViewModelResponse>>> getHalls()
        {
            List<HallViewModelResponse> hallViewModels = new List<HallViewModelResponse>();
            IEnumerable<Hall> halls =await  _hallRepo.GetFirst20RowsAsync();
            foreach(var item in halls)
            {
                
                    ApplicationUser user = await _currentUser.FindByCondition(x=>x.Id == item.UserId);


                hallViewModels.Add(new HallViewModelResponse
                {
                    Id = item.Id,
                    UserImage = user.UserImage,
                    UserName = user.FirstName + " " + user.SecondName,
                    ServiceTitle = item.ServiceTitle,
                    UserId = user.Id,
                    HallDescription = item.HallDescription,
                    TimeAdd = item.TimeAdd
                }); 
            }
            return Ok(hallViewModels);
        }

     [HttpGet("gethall")]
     public async  Task<ActionResult<HallDetailResponse>> GetAHall([FromQuery] string id)
     {
            Hall hall =await _hallRepo.FindByCondition(x => x.Id == id);
            if(hall != null)
            {
                var user = await _currentUser.FindByCondition(x => x.Id == hall.UserId);
                IEnumerable<Offers> offers = _offerRepo.GetTableAsync().Where(x => x.HallID == id).ToList();
                List<OfferViewModel> offerViewModels = new List<OfferViewModel>();
                if (offers.Count() != 0)
                {
                    foreach (var item in offers)
                    {
                        var ownerOffer = await _currentUser.FindByIdAsync(item.UserId);
                        offerViewModels.Add(
                            new OfferViewModel
                            {
                                Budget = item.Budget,
                                TextOffer = item.TextOffer,
                                AddTime = item.AddTime,
                                UserImage = ownerOffer.UserImage,
                                UserName = ownerOffer.FirstName + " " + ownerOffer.LastName,
                                UserId = ownerOffer.Id
                                 

                            }
                            );
                    }
                }
                HallDetailResponse hallDetail = new HallDetailResponse()
                {
                    HallDescription = hall.HallDescription,
                    Budget = hall.Budget,
                    ServiceTitle = hall.ServiceTitle,
                    UserCity = hall.UserCity,
                    UserContact = hall.UserContact,
                    GuestNumber = hall.GuestNumber,
                    Menue = hall.Menue,
                    BookDate = hall.BookDate,
                    UserImage = user.UserImage,
                    UserName = user.FirstName +" "+user.SecondName,
                    Id = hall.Id,
                    OffersPosted = offerViewModels

                };
                return StatusCode(200,hallDetail);
            }
            return StatusCode(205);
     }

    }
}
