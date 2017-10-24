using System;
using System.Threading.Tasks;
using AdPortal.Core.Domain;
using AdPortal.Core.Repositories;

namespace AdPortal.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<User> GetOrFailAsync(this IUserRepository repository, Guid userId)
        {
            var user = await repository.GetAsync(userId);
            if(user == null)
            {
                throw new ArgumentNullException("User does not exist.");
            }  
            return user;
        }

        public static async Task<Category> GetOrFailAsync(this ICategoryRepository repository, Guid categoryId)
        {
            var category = await repository.GetByIdAsync(categoryId);
            if(category == null)
            {
                throw new ArgumentNullException("Category does not exist.");
            }  
            return category;
        }
    }
}