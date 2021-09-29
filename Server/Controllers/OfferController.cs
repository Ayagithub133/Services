using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
    public class OfferController : ControllerBase
    {

        private readonly IRepository<Offers> _Repo;
        
        private readonly IUserRepository<ApplicationUser> _user;
        public OfferController(IRepository<Offers> Repo, IUserRepository<ApplicationUser> user,
            IHubContext<SignalRHub> hubContext, IHubContext<SignalRHub> hub
            )
        {
            _Repo = Repo;
            _user = user;
           
           
        }

        
        [HttpPost("addoffer")]
        public async Task<ActionResult<UserManagerResponse>> AddOffer([FromBody] OfferViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model != null)
                {

                   
                    Offers offer = new Offers
                    {
                        AddTime = DateTime.Now,
                        Budget = model.Budget,
                        TextOffer = model.TextOffer,
                        UserId = CurrentUser.Id,
                        ServiceID = model.ServiceID,
                        Id = Guid.NewGuid().ToString(),
                        HallID = model.HallID
                    };
                  
                    var result = await _Repo.Add(offer);
                    if (result.IsSuccess)
                        return Ok(new UserManagerResponse
                        {
                            Message = "تمت الاضافة",
                            IsSuccess = true
                        });

                    return Ok(new UserManagerResponse
                    {
                        Message = "لم تتم الاضافة"

                    });
                }
                return Ok(new UserManagerResponse
                {
                    Message = "النموذج المرسل فارغ"
                });
            }
            return BadRequest("يوجد اخطاء فى النموزج المرسل");
        }

       [HttpGet("getoffercomments")]
        public async Task<ActionResult<List<OfferViewModel>>> GetAllComments([FromQuery] string hallid)
        {
            IEnumerable<Offers> offers =  _Repo.GetTableAsync().Where(x => x.HallID == hallid).ToList();
            
            List<OfferViewModel> offerViewModels = new List<OfferViewModel>();
            if (offers.Count() != 0)
            {
                foreach (var item in offers)
                {
                    var user = await _user.FindByCondition(x => x.Id == item.UserId);
                    offerViewModels.Add(
                        new OfferViewModel
                        {
                            Budget = item.Budget,
                            TextOffer = item.TextOffer,
                            AddTime = item.AddTime,
                            UserImage = user.UserImage,
                            UserName = user.FirstName + " " + user.LastName,
                            UserId = user.Id
                        }
                        );
                }
                return Ok(offerViewModels);
            }
            return BadRequest(offers);
        }
            
    }
}
