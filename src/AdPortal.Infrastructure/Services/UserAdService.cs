using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdPortal.Core.Domain;
using AdPortal.Core.Repositories;
using AdPortal.Infrastructure.DTO;
using AdPortal.Infrastructure.Extensions;
using AutoMapper;

namespace AdPortal.Infrastructure.Services
{
    public class UserAdService : IUserAdService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UserAdService(IMapper mapper, IUserRepository userRepository, ICategoryRepository categoryRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public async Task AddAsync(Guid userId, Guid categoryID, string name, string content, DateTime expiryDate)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            var category = await _categoryRepository.GetByIdAsync(categoryID);
            user.AddAd(new Ad(userId,category,name,content,expiryDate));
            await _userRepository.UpdateAsync(user);
        }

        public async Task EditAdAsync(Guid userId, Guid adId, Guid categoryID, string name, string content)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            var category = await _categoryRepository.GetOrFailAsync(categoryID);
            var ad = user.GetAd(adId);
            ad.EditAd(name,content,category);
            await _userRepository.UpdateAsync(user);
        }

        public async Task<AdDTO> GetAdDTOAsync(Guid userId, Guid adId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            var ad = user.Ads.SingleOrDefault(x=>x.Id == adId);
            if(ad == null)
            {
                throw new Exception("Selected ad does not exists.");
            }
            return _mapper.Map<AdDTO>(ad);
        }

        public async Task<IEnumerable<AdDTO>> GetAllAdsDTOAsync(Guid userId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            return  _mapper.Map<IEnumerable<Ad>,IEnumerable<AdDTO>>(user.Ads);
        }

        public async Task RemoveAsync(Guid userId, Guid adId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            user.RemoveAd(adId);
            await _userRepository.UpdateAsync(user);
        }

    
    }
}