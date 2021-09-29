using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Server.DataAccessLayer;
using Services.Server.Features;
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
    public class SearchController : ControllerBase
    {
        private readonly IRepository<Hall> _hallRepo;
        private readonly IUserRepository<ApplicationUser> _user;
        public SearchController (IRepository<Hall> hallRepo,
            IUserRepository<ApplicationUser> user)
        {
            _hallRepo = hallRepo;
            _user = user;
        }


        [HttpGet("Pagined")]
        
        public async Task<ActionResult<IEnumerable<HallViewModelResponse>>> Get([FromQuery] PaginationParameters pagination)
        {
            var queryable = _hallRepo.GetTableAsync();
            if (pagination.ServiceType == 1)
            {
                queryable = _hallRepo.GetTableAsync();
            }
            if (!string.IsNullOrEmpty(pagination.ServiceTitle))
            {
                queryable = queryable.Where(x => x.ServiceTitle.Contains(pagination.ServiceTitle));
            }
            if (!string.IsNullOrEmpty(pagination.Address))
            {
                queryable = queryable.Where(x => x.Location.Contains(pagination.Address));
            }
            if(pagination.BookDate!=DateTime.MinValue)
            {
                queryable = queryable.Where(x => x.BookDate == pagination.BookDate);
            }
            
            await HttpContext.INsertPaginationParametersInResponse(queryable, pagination.QuantityPerPage);
            var entity = await queryable.Paginate(pagination).ToListAsync();

            var hallDetails = new List<HallViewModelResponse>();
            foreach(var item in entity)
            {
                var user = await _user.FindByCondition(x => x.Id == item.UserId);
                
                    hallDetails.Add(new HallViewModelResponse
                    {
                        UserImage = user.UserImage,
                        UserName = user.FirstName + " " + user.SecondName,

                        HallDescription = item.HallDescription,
                        ServiceTitle = item.ServiceTitle,
                        TimeAdd = item.TimeAdd,
                        Id = item.Id
                    });
            
            }
            if (hallDetails.Count == 0)
            {
                return StatusCode(204);
            }
            else
            {
                return Ok(hallDetails);
            }

        }

    }
}
