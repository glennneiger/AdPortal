using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdPortal.Infrastructure.DTO;

namespace AdPortal.Infrastructure.Services
{
    public interface IAdsService : IService
    {
        Task<IEnumerable<AdDTO>> BrowseAsync();
        Task<IEnumerable<AdDTO>> BrowseAsync(Guid userId);
        Task<AdDTO> GetAdDTOAsync(Guid adId);
        Task<UserDetailsDTO> GetUserDetailsDTOAsync(Guid userId);
    }
}