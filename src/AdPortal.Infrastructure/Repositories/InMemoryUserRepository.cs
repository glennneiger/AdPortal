using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdPortal.Core.Domain;
using AdPortal.Core.Repositories;

namespace AdPortal.Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private static ISet<User> Users = new HashSet<User>();
        public async Task AddAsync(User user)
            =>await Task.Run(()=> Users.Add(user));

        public async Task<IEnumerable<User>> GetAllAsync()
            => await Task.FromResult(Users);

        public async Task<User> GetAsync(Guid id)
            => await Task.FromResult(Users.SingleOrDefault(x=>x.Id == id));

        public async Task<User> GetAsyncByEmail(string email)
            => await Task.FromResult(Users.SingleOrDefault(x=>x.Email == email.ToLowerInvariant()));
            
        public async Task Remove(Guid id)
        {
            var user = await GetAsync(id);
            Users.Remove(user);
        }

        public async Task UpdateAsync(User user)
        {
            
        }
    }
}