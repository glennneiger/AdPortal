using AdPortal.Core.Domain;
using AdPortal.Infrastructure.Command.UserAd;
using AdPortal.Infrastructure.DTO;
using AutoMapper;

namespace AdPortal.Infrastructure.Mapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            var config = new MapperConfiguration(c => 
            {
                c.CreateMap<User, UserDTO>();
                c.CreateMap<User, UserDetailsDTO>();
                c.CreateMap<Ad, AdDTO>();
                c.CreateMap<Category, CategoryDTO>();

                c.CreateMap<AdDTO,CreateUserAd>().ForMember(x => x.UserId, opt => opt.Ignore());
                c.CreateMap<AdDTO,EditUserAd>().ForMember(x => x.UserId, opt => opt.Ignore());
            });
            return config.CreateMapper();
        }
        
    }
}