using Microsoft.EntityFrameworkCore;
using Services.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Server.DataAccessLayer
{
    public class ServiceRepository : IServicerepository
    {
        private ApplicationDbContext _dbContext;
        public ServiceRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ServicesCategory>> GetAllServicesAsync()
        {
            return await _dbContext.Set<ServicesCategory>().ToListAsync();
        }
    }
}
