using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Server.DataAccessLayer;
using Services.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServicerepository _serviceRepo;
        public ServicesController(IServicerepository serviceRepo)
        {
            _serviceRepo = serviceRepo;
        }

        [HttpGet("getallservices")]
        public async Task<ActionResult<IEnumerable<ServicesViewModel>>> GetServices()
        {
            var services = await _serviceRepo.GetAllServicesAsync();
            List<ServicesViewModel> servicesViewModel = new List<ServicesViewModel>();
            foreach(var item in services)
            {
                servicesViewModel.Add(new ServicesViewModel
                {Id = item.Id,
                ServiceName = item.ServiceName});
            }
            return Ok(servicesViewModel);
        }
    }
}
