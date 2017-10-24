using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdPortal.Core.Repositories;
using AdPortal.Infrastructure.DTO;
using AdPortal.Infrastructure.Extensions;
using AutoMapper;

namespace AdPortal.Infrastructure.Services
{
    public class AdsService : IAdsService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public AdsService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AdDTO>> BrowseAsync()
        {
            var users = await _repository.GetAllAsync();
            var ads = users.SelectMany(x=>x.Ads);

            return _mapper.Map<IEnumerable<AdDTO>>(ads);
        }

        public async Task<IEnumerable<AdDTO>> BrowseAsync(Guid userId)
        {
            var user = await _repository.GetOrFailAsync(userId);

            return _mapper.Map<IEnumerable<AdDTO>>(user.Ads);
        }

        public async Task<AdDTO> GetAdDTOAsync(Guid adId)
        {
            var users = await _repository.GetAllAsync();
            var ad = users.SelectMany(x=>x.Ads).SingleOrDefault(a=>a.Id == adId);
            if(ad == null)
            {
                throw new Exception("Selected ad does not exist.");
            }

            return _mapper.Map<AdDTO>(ad);
        }

        public async Task<UserDetailsDTO> GetUserDetailsDTOAsync(Guid userId)
        {
            var user = await _repository.GetOrFailAsync(userId);

            return _mapper.Map<UserDetailsDTO>(user);
        }
    }
}