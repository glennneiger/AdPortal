using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdPortal.Core.Domain;
using AdPortal.Core.Repositories;
using AdPortal.Infrastructure.DTO;
using AutoMapper;

namespace AdPortal.Infrastructure.Services
{
    public class AdminService : IAdminService
    {
       private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public AdminService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AdDTO>> BrowseAllAsync()
        {
            var users = await _repository.GetAllAsync();
            var ads = users.SelectMany(x=>x.Ads);

            return _mapper.Map<IEnumerable<AdDTO>>(ads);
        }

        public async Task ChangeAdStatusAsync(Guid adId, Status newStatus)
        {
            var users = await _repository.GetAllAsync();
            var ad = users.SelectMany(x=>x.Ads).SingleOrDefault(a=>a.Id == adId);
            if(ad == null)
            {
                throw new Exception("Selected ad does not exist.");
            }
            ad.Status = newStatus;
            await _repository.UpdateAsync(users.First(x=>x.Ads.Contains(ad)));
        }
    }
}