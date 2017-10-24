using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdPortal.Infrastructure.DTO;

namespace AdPortal.Infrastructure.Services
{
    public interface ICategoryService : IService
    {
         Task<IEnumerable<CategoryDTO>> BrowseDTOAsync();
         Task AddCategoryAsync(Guid id, string name, string description);
         Task<CategoryDTO> GetDTOById(Guid id);
    }
}