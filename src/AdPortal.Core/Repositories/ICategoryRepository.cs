using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdPortal.Core.Domain;

namespace AdPortal.Core.Repositories
{
    public interface ICategoryRepository : IRepository
    {
        Task<IEnumerable<Category>> BrowseAsync();
        Task<Category> GetByIdAsync(Guid id);
        Task AddCategoryAsync(Category category);
    }
}