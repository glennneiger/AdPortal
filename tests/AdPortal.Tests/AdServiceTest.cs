using System;
using System.Threading.Tasks;
using AdPortal.Core.Domain;
using AdPortal.Core.Repositories;
using AdPortal.Infrastructure.Services;
using AutoMapper;
using Moq;
using Xunit;

namespace AdPortal.Tests
{
    public class AdServiceTest
    {

        private User _testUser 
            => new User(Guid.NewGuid(),"testuser@email.com","testuser","secret","salt","user");
            
        [Fact]
        public async Task browse_async_should_invoke_get_all_async()
        {
            var userRepository = new Mock<IUserRepository>();
            var mapper = new Mock<IMapper>();

            var adService = new AdsService(userRepository.Object,mapper.Object);
            var users = await adService.BrowseAsync();

            userRepository.Verify(x => x.GetAllAsync(),Times.Once);
        }

        [Fact]
        public async Task browse_async_with_id_should_invoke_get_async_by_id()
        {
            var guid = Guid.NewGuid();
            var userRepository = new Mock<IUserRepository>();
            var mapper = new Mock<IMapper>();

            var adService = new AdsService(userRepository.Object,mapper.Object);
            //user is null
            await Assert.ThrowsAsync<ArgumentNullException>(
            async ()=>  await adService.BrowseAsync(guid)
            );
           
            userRepository.Verify(x => x.GetAsync(It.Is<Guid>(y=>y==guid)),Times.Once);
        }
    }
}