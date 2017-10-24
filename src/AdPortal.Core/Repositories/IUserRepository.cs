using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdPortal.Core.Domain;

namespace AdPortal.Core.Repositories 
{
    public interface IUserRepository : IRepository
    {
         Task<User> GetAsync(Guid id);
         Task<User> GetAsyncByEmail(string email);
         Task AddAsync(User user);
         Task UpdateAsync(User user);
         Task Remove(Guid id);
         Task<IEnumerable<User>> GetAllAsync();
    }
}