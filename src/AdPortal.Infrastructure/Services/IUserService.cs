using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdPortal.Core.Domain;
using AdPortal.Infrastructure.DTO;

namespace AdPortal.Infrastructure.Services
{
    public interface IUserService : IService
    {
         Task LoginAsync(string email, string password);
         Task RegisterAsync(string name , string email, string password, Role role);
         Task<UserDTO> GetUserDTO(Guid userId);
         Task<UserDTO> GetUserDTO(string email);
         Task<IEnumerable<UserDTO>> BrowswseDTOAsync();
         Task<UserDetailsDTO> GetUserDetailsDTO(Guid userId);
        // Task EditUser(Guid userId, string email, string username);
         Task ChangePassword(Guid userId, string newpassword, string oldpassword);

    }
}