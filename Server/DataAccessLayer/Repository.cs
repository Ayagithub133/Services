using Microsoft.EntityFrameworkCore;
using Services.Server.Models;
using Services.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.Server.DataAccessLayer
{
    public class Repository<T> : IRepository<T> where T :BaseEntity
    {
        private ApplicationDbContext _dbContext;
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserManagerResponse> Add(T entity)
        {

            _dbContext.Set<T>().Add(entity);
            try
            {
                
                await _dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                var x1 = ex.Message;
                var x2 = ex.Source;
                var x3 = ex.InnerException;
            }
        
          
            return new UserManagerResponse()
            {
                Message = "تمت الاضافة بنجاح",
                IsSuccess = true
            };
        }

        public async Task<int> CountOfRows(string id)
        {
            return await _dbContext.Set<T>().Where(x=>x.Id == id).CountAsync();
        }

        public async Task Delete(Guid id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> FindByCondition(Expression<Func<T, bool>> predicate)
        {
          return await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }
        public async Task<IEnumerable<T>> FindAllByCondition(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
           return await _dbContext.Set<T>().ToListAsync();
        }


        public async Task<IEnumerable<T>> GetFirst20RowsAsync()
        {

            var result = await _dbContext.Set<T>().Take(20).ToListAsync();
            return result;
        }

        public  IQueryable<T> GetTableAsync()
        {
            return  _dbContext.Set<T>();
        }

        public async Task Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
