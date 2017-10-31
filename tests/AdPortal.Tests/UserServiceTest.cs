using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdPortal.Core.Domain;
using AdPortal.Core.Repositories;
using AdPortal.Infrastructure.DTO;
using AdPortal.Infrastructure.Repositories;
using AdPortal.Infrastructure.Services;
using AutoMapper;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace AdPortal.Tests
{
    public class UserServiceTest
    {

        private User _testUser 
            => new User(Guid.NewGuid(),"testuser@email.com","testuser","secret","salt","user"); 

        [Fact]
        public async Task register_async_should_invoke_add_async_on_repository()
        {
            var userRepository = new Mock<IUserRepository>();
            var encreypter = new Mock<IEncrypter>();
            var mapper = new Mock<IMapper>();

            var userService = new UserService(userRepository.Object,mapper.Object,encreypter.Object);
            await userService.RegisterAsync(_testUser.UserName,_testUser.Email,_testUser.Password,_testUser.Role);

            userRepository.Verify(x => x.AddAsync(It.IsAny<User>()),Times.Once);
        }

        [Fact]
        public async Task register_async_should_throw_exception_when_email_is_already_in_use()
        {
            var encreypterMock = new Mock<IEncrypter>();
            var mapperMock = new Mock<IMapper>();
            var userRepositoryMock = new Mock<IUserRepository>();
            
            var userService = new UserService(userRepositoryMock.Object,mapperMock.Object,encreypterMock.Object);

            await userService.RegisterAsync(_testUser.UserName,_testUser.Email,_testUser.Password,_testUser.Role);
            var users = await userService.BrowswseDTOAsync();
           
            userRepositoryMock.Setup(x => x.GetAsyncByEmail(It.IsAny<string>())).ReturnsAsync(_testUser);
           
            var exception = await Assert.ThrowsAnyAsync<Exception>(
                async() => await userService.RegisterAsync(
                    "",_testUser.Email,"","")
            );
            Assert.Equal($"User with email: '{_testUser.Email}' exists.",exception.Message);
        }
        [Fact]
        public async Task browsedto_async_should_invoke_get_all_async()
        {
            var userRepository = new Mock<IUserRepository>();
            var encreypter = new Mock<IEncrypter>();
            var mapper = new Mock<IMapper>();

            var userService = new UserService(userRepository.Object,mapper.Object,encreypter.Object);
            var users = await userService.BrowswseDTOAsync();

            userRepository.Verify(x => x.GetAllAsync(),Times.Once);
        }

        [Fact]
        public async Task change_password_showud_throw_excepeion_when_new_passwors_is_the_same_as_old()
        {
            var encreypterMock = new Mock<IEncrypter>();
            var mapperMock = new Mock<IMapper>();
            var userRepositoryMock = new Mock<IUserRepository>();

            var userService = new UserService(userRepositoryMock.Object,mapperMock.Object,encreypterMock.Object);
            
            userRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(_testUser);
            encreypterMock.Setup(x => x.GetHash(It.IsAny<String>(),It.IsAny<String>())).Returns(_testUser.Password);

            var exception = await Assert.ThrowsAnyAsync<Exception>(
                async() => await userService.ChangePassword(
                    _testUser.Id,_testUser.Password,_testUser.Password)
            );
            Assert.Equal("Can not change password. New password must be not equal current."
                ,exception.Message);
        }

    }
}