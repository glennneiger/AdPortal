using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdPortal.Core.Domain;
using AdPortal.Core.Repositories;
using AdPortal.Infrastructure.DTO;
using AdPortal.Infrastructure.Extensions;
using AutoMapper;

namespace AdPortal.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEncrypter _encrypter;

        public UserService (IUserRepository repository, IMapper mapper,IEncrypter encrypter)
        {
            _userRepository = repository;
            _mapper = mapper;
            _encrypter = encrypter;
        }

        public async Task<IEnumerable<UserDTO>> BrowswseDTOAsync()
        {
            var users =  await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<User>,IEnumerable<UserDTO>>(users);
        }

        public async Task ChangePassword(Guid id, string newpassword, string oldpassword)
        {
            var user = await _userRepository.GetOrFailAsync(id);
            var hashold = _encrypter.GetHash(oldpassword,user.Salt);
            if(user.Password != hashold)
            {
               throw new Exception("Password is wrong."); 
            }

            var hashnew = _encrypter.GetHash(newpassword,user.Salt);
            if(user.Password == hashnew)
            {
                throw new Exception("Can not change password. New password must be not equal current.");
            }
            user.ChangePassword(hashnew);
            await _userRepository.UpdateAsync(user);

        }
/* 
       public async Task EditUser(Guid userId, string email, string username)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            user.Edit(email, username);
            await _userRepository.UpdateAsync(user);
        }
*/
        public async Task<UserDetailsDTO> GetUserDetailsDTO(Guid id)
        {
            var user = await _userRepository.GetAsync(id);
            return  _mapper.Map<User,UserDetailsDTO>(user);
        }

        public async Task<UserDTO> GetUserDTO(Guid id)
        {
            var user = await _userRepository.GetAsync(id);
            return  _mapper.Map<User,UserDTO>(user);
        }

        public async Task<UserDTO> GetUserDTO(string email)
        {
            var user = await _userRepository.GetAsyncByEmail(email);
            return  _mapper.Map<User,UserDTO>(user);
        }

        public async Task LoginAsync(string email, string password)
        {
            if(email == null || password == null)
            {
                throw new Exception("Invalid credentials. Fill email and password.");
            }
            var user = await _userRepository.GetAsyncByEmail(email);
            if(user == null)
            {
                throw new Exception("Invalid credentials.");
            }
            var hash = _encrypter.GetHash(password,user.Salt);
            if(user.Password == hash)
            {
                return;
            }
            throw new Exception("Invalid credentials.");
        }

        public async Task RegisterAsync(string name, string email, string password, string role)
        {
            var user = await _userRepository.GetAsyncByEmail(email);
            if(user != null)
            {
                throw new Exception($"User with email: '{email}' exists.");
            }
            var salt = _encrypter.GetSalt(password);
            var hash = _encrypter.GetHash(password,salt);
            var newuser = new User(Guid.NewGuid(),email,name,hash,salt, role);
            await _userRepository.AddAsync(newuser);
        }
    }
}