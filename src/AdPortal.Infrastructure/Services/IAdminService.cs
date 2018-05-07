using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdPortal.Core.Domain;
using AdPortal.Infrastructure.DTO;

namespace AdPortal.Infrastructure.Services
{
    public interface IAdminService : IService
    {
        Task ChangeAdStatusAsync(Guid adId, Status newStatus);
        Task<IEnumerable<AdDTO>> BrowseAllAsync();
    }
}