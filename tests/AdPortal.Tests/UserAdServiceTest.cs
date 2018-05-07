using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdPortal.Core.Domain;
using AdPortal.Core.Repositories;
using AdPortal.Infrastructure.Services;
using AutoMapper;
using Moq;
using Xunit;

namespace AdPortal.Tests
{
    public class UserAdServiceTest
    {
        private Category _testCategory =>
            new Category(Guid.NewGuid(),"testCategory","testDescription");
        private Ad _testAd =>
            new Ad(Guid.NewGuid(),_testCategory,"testName","testContent",DateTime.UtcNow);
        private User _testUser 
            => new User(Guid.NewGuid(),"testuser@email.com","testuser","secret","salt","user");

        [Fact]
        public async Task edit_ad_async_should_throw_exception_when_selected_ad_does_not_exists()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            var mapperMock = new Mock<IMapper>();

            var userAdService = new UserAdService(mapperMock.Object,userRepositoryMock.Object,categoryRepositoryMock.Object);
            userRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(_testUser);
            categoryRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(_testCategory);
            
            var exception = await Assert.ThrowsAnyAsync<Exception>(
                async () => await userAdService.EditAdAsync(_testUser.Id,_testAd.Id,_testCategory.Id,_testCategory.Name,_testCategory.Description)
            );

             Assert.Equal("Can not find selected ad."
                ,exception.Message);    
        }
    }
}