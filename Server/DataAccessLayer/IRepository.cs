using Services.Server.Models;
using Services.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.Server.DataAccessLayer
{
    public interface IRepository<T> where T: BaseEntity
    {
        public Task<UserManagerResponse> Add(T entity);
        public Task Delete(Guid id);
        public Task Update(T entity);
        public Task<T> FindByCondition(Expression<Func<T, bool>> predicate);
        public Task<IEnumerable<T>> GetAll();
        public Task<IEnumerable<T>> GetFirst20RowsAsync();
        public IQueryable<T> GetTableAsync();
        public Task<int> CountOfRows(string id);
        public Task<IEnumerable<T>> FindAllByCondition(Expression<Func<T, bool>> predicate);

    }
}
