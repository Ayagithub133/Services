using Services.Server.Models;
using Services.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.Server.DataAccessLayer
{
    public interface IUserRepository<T> 
    {
        public Task<UserManagerResponse> LoginUserAsync(LoginViewModel model);
        public Task<UserManagerResponse> RegisterUserAsyn(RegisterViewModel registerModel);
        public Task<UserManagerResponse> Add(T entity);
        public Task Delete(Guid id);
        public Task<UserManagerResponse> Update(T entity);
        public Task UpdateByIdAsync(string id);
        public Task<T> FindByCondition(Expression<Func<T, bool>> predicate);
        public T FindEntityByCondition(Expression<Func<T, bool>> predicate);
        public Task<IEnumerable<T>> GetAll();
        public Task<T> FindByIdAsync(string Id);
        public Task<UserManagerResponse> ConfirmEmailAsync(string userId);
        public Task ActivationUpdateAsync(string id, bool value);
    }
}
