using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdPortal.Core.Domain;
using AdPortal.Infrastructure.DTO;

namespace AdPortal.Infrastructure.Services
{
    public interface IUserAdService : IService
    {
        Task EditAdAsync(Guid userId, Guid adId, Guid categoryID, string name, string content);
        Task AddAsync(Guid userID, Guid categoryID, string name, string content, DateTime expiryDate);
        Task RemoveAsync(Guid userId, Guid adId);
        Task<AdDTO> GetAdDTOAsync(Guid userId, Guid adId);
        Task<IEnumerable<AdDTO>> GetAllAdsDTOAsync(Guid UserId);
         
    }
}