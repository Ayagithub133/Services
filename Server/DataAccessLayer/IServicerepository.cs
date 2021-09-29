using Services.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Server.DataAccessLayer
{
    public interface IServicerepository
    {
        public Task<IEnumerable<ServicesCategory>> GetAllServicesAsync();
    }
}
