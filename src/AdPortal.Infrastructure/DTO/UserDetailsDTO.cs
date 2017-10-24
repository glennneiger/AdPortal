using System.Collections.Generic;

namespace AdPortal.Infrastructure.DTO
{
    public class UserDetailsDTO : UserDTO
    {
        public IEnumerable<AdDTO> Ads {get;set;}
    }
}